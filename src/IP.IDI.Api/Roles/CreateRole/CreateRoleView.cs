namespace IP.IDI.Api.Roles.CreateRole;

public sealed record CreateRoleCommand(CreateRoleRequest Request) :
    ICommand<CreateRoleResponse>, ILoggableData;

public sealed class CreateRoleRequest
{
    [Required]
    [MinLength(AppRole.NAME_MIN_LENGTH)]
    [MaxLength(AppRole.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

}

public sealed class CreateRoleResponse(Guid id, string name, string message) :
    ResolvedData<CreateRoleResponseData>(new CreateRoleResponseData(id, name), message);

public sealed record CreateRoleResponseData(Guid Id, string Name);