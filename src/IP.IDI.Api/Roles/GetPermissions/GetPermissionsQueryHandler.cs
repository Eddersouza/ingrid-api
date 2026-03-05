namespace IP.IDI.Api.Roles.GetPermissions;

internal class GetPermissionsQueryHandler :
    IQueryHandler<GetPermissionsQuery, GetPermissionsResponse>
{
    public async Task<Result<GetPermissionsResponse>> Handle(
        GetPermissionsQuery query,
        CancellationToken cancellationToken)
    {   
        GetPermissionsResponse response = 
            new(ClaimsInfo.GetPermissionValueList());

        return await Task.FromResult(response);
    }
}