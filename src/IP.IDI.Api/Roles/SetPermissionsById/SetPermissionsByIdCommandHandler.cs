namespace IP.IDI.Api.Roles.SetPermissionsById;

internal sealed class SetPermissionsByIdCommandHandler(IIDIUnitOfWork _unitOfWork) :
    ICommandHandler<SetPermissionsByIdCommand, SetPermissionsByIdResponse>
{
    private readonly IAppRoleRepository _roleRepository =
        _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<SetPermissionsByIdResponse>> Handle(
        SetPermissionsByIdCommand command,
        CancellationToken cancellationToken)
    {
        AppRole? role = await _roleRepository.Data()
            .Include(rc => rc.RoleClaims)
            .FirstOrDefaultAsync(role => role.Id == command.Id,
            cancellationToken);

        if (role is null) return RoleErrors.NotFound;

        role.RoleClaims = [.. role.RoleClaims
            .Where(rc => rc.RoleId == role.Id &&
                rc.ClaimType != JwtCustomClaimNames.Permission)];

        _roleRepository.Update(role);

        foreach (var permission in command.Request.Permissions)
        {
            var roleClaim = new AppRoleClaim()
            {
                RoleId = role.Id,
                ClaimType = JwtCustomClaimNames.Permission,
                ClaimValue = permission
            };

            role.RoleClaims.Add(roleClaim);
        }

        _roleRepository.Update(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new SetPermissionsByIdResponse(
            role.Id,
            role.Name!,
            $"Registro de Perfil alterado com sucesso!");

        return Result.Success(response);
    }
}
