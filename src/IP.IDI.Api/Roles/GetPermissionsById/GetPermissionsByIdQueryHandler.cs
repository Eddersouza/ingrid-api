namespace IP.IDI.Api.Roles.GetPermissionsById;

internal class GetPermissionsByIdQueryHandler(IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetPermissionsByIdQuery, GetPermissionsByIdResponse>
{
    private readonly IAppRoleRepository _roleRepository =
      _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<GetPermissionsByIdResponse>> Handle(
        GetPermissionsByIdQuery query,
        CancellationToken cancellationToken)
    {
        AppRole? currentRole =
           await _roleRepository.Data()
           .Include(role => role.RoleClaims)
           .AsNoTracking()
           .FirstOrDefaultAsync(role => role.Id == query.Id, cancellationToken);

        if (currentRole is null) return RoleErrors.NotFound;

        IEnumerable<string> listClaims = 
            currentRole.RoleClaims.Select(roleClaim => roleClaim.ClaimValue!);

        GetPermissionsByIdResponse response = new(listClaims);

        return await Task.FromResult(response);
    }
}