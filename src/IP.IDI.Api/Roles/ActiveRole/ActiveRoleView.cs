using System.ComponentModel;

namespace IP.IDI.Api.Roles.ActiveRole;

public sealed record ActiveRoleCommand(Guid Id, ActiveRoleRequest Request) :
    ICommand<ActiveRoleResponse>, ILoggableData;

public sealed class ActiveRoleRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ActiveRoleResponse(Guid id, string name, bool active, string message) :
    ResolvedData<ActiveRoleResponseData>(
        new ActiveRoleResponseData(id, name, active), message);

public sealed record ActiveRoleResponseData(Guid Id, string Name, bool Active);