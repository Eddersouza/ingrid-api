namespace IP.IDI.Api.Roles.CreateRole;

internal sealed class CreateRoleCommandHandler(IIDIUnitOfWork _unitOfWork) : 
    ICommandHandler<CreateRoleCommand, CreateRoleResponse>
{
    private readonly IAppRoleRepository _roleRepository =
        _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<CreateRoleResponse>> Handle(
        CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Name == command.Request.Name,
            cancellationToken);

        if (role is not null) return RoleErrors.RoleAlreadyExists;

        role = AppRole.Create(command.Request.Name);

        await _roleRepository.Create(role);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateRoleResponse(
            role.Id,
            role.Name!,
            $"Registro de Role [{role}] criado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}