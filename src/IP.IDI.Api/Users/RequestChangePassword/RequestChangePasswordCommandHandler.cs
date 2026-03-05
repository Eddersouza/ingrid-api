using System.Web;

namespace IP.IDI.Api.Users.RequestChangePassword;

internal class RequestChangePasswordCommandHandler(
    ICurrentUserInfo _currentUser,
    IPasswordHasher _passwordHasher,
    IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<RequestChangePasswordCommand, RequestChangePasswordResponse>
{
    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<RequestChangePasswordResponse>> Handle(
        RequestChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        AppUser? user = await _userRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (user is null) return UserErrors.NotFound;

        bool hasPermission = 
            _currentUser.HasPermission(UserClaim.CanChangePassword.Claim) ||
            _currentUser.Permissions.Contains(UserClaim.All.Claim) ||
            _currentUser.Id == user.Id;

        if (!hasPermission) return UserErrors.ForbiddenAccess;

        bool requestedByAdm = user.Id != _currentUser.Id;

        string token = _passwordHasher
            .Hash($"{user.UserName}{user.Email}{user.PasswordHash}");

        string tokenEncoded = HttpUtility.UrlEncode(token);

        user.EventsInfo.Raise(
            new RequestedChangePasswordDomainEvent(
                user.Id,
                user.UserName!,
                user.Email!,
                tokenEncoded,
                requestedByAdm));

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RequestChangePasswordResponse(
            new RequestChangePasswordUserResponse(user.Id, user.UserName!, user.Email!),
            $"Troca de senha para [{user}] requisitada com sucesso!");

        return Result.Success(response);
    }
}
