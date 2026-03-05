namespace IP.IDI.Api.Roles.GetPermissionsById;

public sealed record GetPermissionsByIdQuery(Guid Id) :
    IQuery<GetPermissionsByIdResponse>, ILoggableData;

public sealed class GetPermissionsByIdResponse(
    IEnumerable<string> permissions) :
    ResolvedData<GetPermissionsByIdData>(
        new GetPermissionsByIdData(permissions), string.Empty);

public sealed record GetPermissionsByIdData(IEnumerable<string> Permissions);