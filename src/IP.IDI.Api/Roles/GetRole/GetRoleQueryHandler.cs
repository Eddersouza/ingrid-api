namespace IP.IDI.Api.Roles.GetRole;

internal sealed class GetRoleQueryHandler(
    IIDIUnitOfWork _unitOfWork) :
    IQueryHandler<GetRoleQuery, GetRoleResponse>
{
    private readonly IAppRoleRepository _roleRepository =
        _unitOfWork.GetRepository<IAppRoleRepository>();

    public async Task<Result<GetRoleResponse>> Handle(
        GetRoleQuery query,
        CancellationToken cancellationToken)
    {
        AppRole? currentRole =
            await _roleRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(role => role.Id == query.Id, cancellationToken);

        if (currentRole is null) return RoleErrors.NotFound;

        GetRoleResponse response = new(
            currentRole.Id!,
            currentRole.Name!,
            currentRole.ActivableInfo.Active);

        return Result.Success(response);
    }
}