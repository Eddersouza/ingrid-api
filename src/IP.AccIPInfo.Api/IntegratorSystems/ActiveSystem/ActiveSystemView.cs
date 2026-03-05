namespace IP.AccIPInfo.Api.IntegratorSystems.ActiveSystem;

public sealed record ActiveSystemCommand(Guid Id, ActiveSystemRequest Request) :
    ICommand<ActiveSystemResponse>, ILoggableData;

public sealed class ActiveSystemRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ActiveSystemResponse(Guid id, string name, bool active, string message) :
    ResolvedData<ActiveSystemResponseData>(
        new ActiveSystemResponseData(id, name, active), message);

public sealed record ActiveSystemResponseData(Guid Id, string Name, bool Active);