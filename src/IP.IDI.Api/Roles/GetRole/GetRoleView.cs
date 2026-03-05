namespace IP.IDI.Api.Roles.GetRole;

public sealed record GetRoleQuery(Guid Id) :
    IQuery<GetRoleResponse>, ILoggableData;

public sealed class GetRoleResponse(
    Guid id, string name, bool active) :
    ResolvedData<GetRoleResponseData>(new GetRoleResponseData(id, name, active), string.Empty);

public sealed record GetRoleResponseData(Guid Id, string Name, bool Active);