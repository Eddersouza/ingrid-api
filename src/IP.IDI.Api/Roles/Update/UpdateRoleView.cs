namespace IP.IDI.Api.Roles.Update;

public sealed record UpdateRoleCommand(Guid Id, UpdateRoleRequest Request) :
    ICommand<UpdateRoleResponse>, ILoggableData;

public sealed class UpdateRoleRequest
{
    [Required]
    [MinLength(AppRole.NAME_MIN_LENGTH)]
    [MaxLength(AppRole.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

}

public sealed class UpdateRoleResponse(Guid id, string name, string message) :
    ResolvedData<UpdateRoleResponseData>(new UpdateRoleResponseData(id, name), message);

public sealed record UpdateRoleResponseData(Guid Id, string Name);