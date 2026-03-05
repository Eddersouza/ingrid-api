namespace IP.IDI.Api.Roles.Update;

internal sealed class UpdateRoleCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<UpdateRoleCommand, UpdateRoleResponse>
{
    private readonly IAppRoleRepository _roleRepository =
        _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<UpdateRoleResponse>> Handle(
        UpdateRoleCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (role is null) return RoleErrors.NotFound;

        role.Name = command.Request.Name;

        _roleRepository.Update(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateRoleResponse(
            role.Id,
            role.Name!,
            $"Registro de Perfil alterado com sucesso!");

        return Result.Success(response);
    }
}
