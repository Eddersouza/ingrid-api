namespace IP.IDI.Api.Roles.GetPermissions;

public sealed record GetPermissionsQuery :
    IQuery<GetPermissionsResponse>, ILoggableData;

public sealed class GetPermissionsResponse(
    IEnumerable<string> permissions) :
    ResolvedData<GetPermissionsData>(
        new GetPermissionsData(permissions), string.Empty);

public sealed record GetPermissionsData(IEnumerable<string> Permissions);