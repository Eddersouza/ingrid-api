namespace IP.IDI.Api.Roles.ActiveRole;

internal sealed class ActiveRoleCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<ActiveRoleCommand, ActiveRoleResponse>
{
    private readonly IAppRoleRepository _roleRepository =
        _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<ActiveRoleResponse>> Handle(
        ActiveRoleCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (role is null) return RoleErrors.NotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if(isActived && role.ActivableInfo.Active) 
            return RoleErrors.AlreadyActiveStatus(isActived);

        if (isActived) role.ActivableInfo.SetAsActive();
        else role.ActivableInfo.SetAsDeactive(reason);

        _roleRepository.Update(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveRoleResponse(
            role.Id,
            role.Name!,
            role.ActivableInfo.Active,
            $"Registro de Perfil {actionText} com sucesso!");

        return Result.Success(response);
    }
}