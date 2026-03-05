namespace IP.AccIPInfo.Api.AccountSubscriptions.ActiveAccountSubscription;

public sealed record ActiveAccountSubscriptionCommand(Guid Id, ActiveAccountSubscriptionRequest Request) :
    ICommand<ActiveAccountSubscriptionResponse>, ILoggableData;

public sealed class ActiveAccountSubscriptionRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ActiveAccountSubscriptionResponse(Guid id, string name, bool active, string message) :
    ResolvedData<ActiveAccountSubscriptionResponseData>(
        new ActiveAccountSubscriptionResponseData(id, name, active), message);

public sealed record ActiveAccountSubscriptionResponseData(Guid Id, string Name, bool Active);

