namespace IP.IDI.Api.Users.Confirm;

internal sealed class ConfirmUserCommandHandler(
    IIDIUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher) :
    ICommandHandler<ConfirmUserCommand, ConfirmUserResponse>
{
    private readonly IAppUserRepository _userRepository =
            _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<ConfirmUserResponse>> Handle(
        ConfirmUserCommand command,
        CancellationToken cancellationToken)
    {
        string password = command.Request.Password;
        string confirmPassword = command.Request.ConfirmPassword;
        string token = command.Request.Token;

        AppUser? user = await _userRepository.Data()
            .Include(user => user.UserRoles)
            .ThenInclude(userRole => userRole.Role)
            .FirstOrDefaultAsync(user => user.Id == command.Id,
               cancellationToken);

        if (user is null) return UserErrors.NotFound;

        ErrorValidation errors = UserValidation.ValidatePassord(password);

        if (errors.Errors.Any())
            return errors;

        string currentData = $"{user.UserName}{user.Email}{user.PasswordHash}";
        bool tokenIsValid = _passwordHasher.Verify(token, currentData);

        if(!tokenIsValid) return UserErrors.ConfirmUserInvalidToken;

        user.PasswordHash = _passwordHasher.Hash(password);
        user.EmailConfirmed = true;

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        ConfirmUserResponse response = ConfirmResponse(user);

        return await Task.FromResult(Result.Success(response));
    }

    private static ConfirmUserResponse ConfirmResponse(AppUser user)
    {
        AppRole role = user.UserRoles.First().Role;

        UserResponseData data = new(
                user!.Id,
                user.UserName!,
                user.Email!,
                new UserRoleResponseData(
                    role.Id,
                    role.Name!),
                user.ActivableInfo.Active,
                user.AuditableInfo.CreatedDate.ToString("G"),
                user.EmailConfirmed);

        ConfirmUserResponse response =
           new(data,
           $"Registro de Usuário [{user}] confirmado com sucesso!");
        return response;
    }
}