namespace IP.IDI.Api.Roles.SetPermissionsById;

public sealed record SetPermissionsByIdCommand(Guid Id, SetPermissionsByIdRequest Request) :
    ICommand<SetPermissionsByIdResponse>, ILoggableData;

public sealed class SetPermissionsByIdRequest
{    
    public List<string> Permissions { get; set; } = [];
}

public sealed class SetPermissionsByIdResponse(Guid id, string name, string message) :
    ResolvedData<SetPermissionsByIdResponseData>(new SetPermissionsByIdResponseData(id, name), message);

public sealed record SetPermissionsByIdResponseData(Guid Id, string Name);