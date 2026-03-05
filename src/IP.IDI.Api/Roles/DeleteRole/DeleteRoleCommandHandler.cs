namespace IP.IDI.Api.Roles.DeleteRole;

internal sealed class DeleteRoleCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<DeleteRoleCommand, DeleteRoleResponse>
{
    private readonly IAppRoleRepository _roleRepository =
        _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<DeleteRoleResponse>> Handle(
        DeleteRoleCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (role is null) return RoleErrors.NotFound;

        role.DeletableInfo.SetReason(command.Request.Reason);

        _roleRepository.Delete(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteRoleResponse(
            $"Registro de Perfil [{role}] excluído com sucesso!");

        return Result.Success(response);
    }
}
