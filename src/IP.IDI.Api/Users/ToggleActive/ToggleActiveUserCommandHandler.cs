namespace IP.IDI.Api.Users.ToggleActive;

internal sealed class ToggleActiveUserCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<ToggleActiveUserCommand, ToggleActiveUserResponse>
{

    private readonly IAppUserRepository _userRepository =
        _unitOfWork.GetRepository<IAppUserRepository>();

    public async Task<Result<ToggleActiveUserResponse>> Handle(
        ToggleActiveUserCommand command,
        CancellationToken cancellationToken)
    {
        AppUser? user = await _userRepository.Data()
            .Include(x => x.UserRoles).ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (user is null) return UserErrors.NotFound;

        bool isActived = command.Request.Active;

        if (isActived && user.ActivableInfo.Active)
            return UserErrors.AlreadyActiveStatus(isActived);

        if (isActived) user.ActivableInfo.SetAsActive();
        else user.ActivableInfo.SetAsDeactive(command.Request.Reason);

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        string activeDeactive = isActived ? "ativado" : "desativado";

        string message = $"Registro de {user.ToEntityName()} [{user}] {activeDeactive} com sucesso!";

        UserResponseData data = new(
               user!.Id,
               user.UserName!,
               user.Email!,
               new UserRoleResponseData(
                   user.UserRoles!.First().Role!.Id,
                   user.UserRoles!.First().Role!.Name!),
               user.ActivableInfo.Active,
               user.AuditableInfo.CreatedDate.ToString("G"),
               user.EmailConfirmed);

        var response = new ToggleActiveUserResponse(data, message);

        return Result.Success(response);
    }
}