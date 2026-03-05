using Microsoft.Extensions.Options;

namespace IP.IDI.Api.Users.Authenticate;

internal class AuthenticateUserCommandHandler(
    IOptions<AuthConfigurationOptions> optionsConfiguration,
    IIDIUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher,
    ITokenProvider _tokenProvider) :
    ICommandHandler<AuthenticateUserCommand, AuthenticateUserResponse>
{
    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    private readonly AuthConfigurationOptions _authConfig =
        optionsConfiguration.Value;

    public async Task<Result<AuthenticateUserResponse>> Handle(
        AuthenticateUserCommand command,
        CancellationToken cancellationToken)
    {
        string userOrEmail = command.Request.UserOrEmail;
        string password = command.Request.Password;

        AppUser? user = await _userRepository.Data()
            .AsNoTracking()
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ThenInclude(x => x.RoleClaims)
            .FirstOrDefaultAsync(x => x.Email == userOrEmail ||
            x.UserName == userOrEmail, cancellationToken);

        if (UserNotFound(user) ||
            !PasswordNotValid(password, user?.PasswordHash ?? string.Empty) ||
            !user!.ActivableInfo.Active)
        {
            await ApplyLockIfNecessary(user, cancellationToken);
            return UserErrors.AuthenticateInfoWrong;
        }

        if (!user!.EmailConfirmed)
        {
            await ApplyLockIfNecessary(user, cancellationToken);
            return UserErrors.EmailNotConfirmed;
        }

        if (user.LockoutEnd.HasValue &&
            user.LockoutEnd > DateTime.UtcNow.AddMinutes(5)) 
            return UserErrors.UserLocked(
                _authConfig.MaxFailedAccessAttempts, 
                _authConfig.DefaultLockoutTimeSpanMinutes);

        string token = CreateToken(user!);
        if (user.AccessFailedCount > 0)
        {
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
        }

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new AuthenticateUserResponse(
            new AuthenticateUserResponseData(
                user!.Id,
                user.UserName!,
                user.Email!,
                user.UserRoles.FirstOrDefault()!.Role.Name!,
                token),
            $"Usuario [{user}] autenticado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }

    private async Task ApplyLockIfNecessary(AppUser? user, CancellationToken cancellationToken)
    {
        if (user!.AccessFailedCount >= _authConfig.MaxFailedAccessAttempts)
        {
            user.LockoutEnd = DateTime.UtcNow.AddMinutes(_authConfig.DefaultLockoutTimeSpanMinutes);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

    private string CreateToken(AppUser user)
    {

        var role = user.UserRoles.FirstOrDefault()?.Role;

        IEnumerable<string> claims = role!.RoleClaims.Select(x => x.ClaimValue!);

        var userAuth = new UserAuth(
           user!.Id,
           user.Email!,
           user.UserName!,
           role!.Name!,
           claims);

        return _tokenProvider.Create(userAuth);
    }

    private static bool UserNotFound(AppUser? user) =>
        user is null;

    private bool PasswordNotValid(string password, string passwordHash) =>
            _passwordHasher.Verify(passwordHash, password);
}

public sealed record UserAuth(Guid Id, string Email, string Name, string Profile, IEnumerable<string> Claims) :
    IAppUserTokenData;