namespace IP.AccIPInfo.Api.AccountSubscriptions.DeleteAccountSubscription;

public sealed record DeleteAccountSubscriptionCommand(Guid Id, DeleteAccountSubscriptionRequest Request) :
    ICommand<DeleteAccountSubscriptionResponse>, ILoggableData;

public sealed class DeleteAccountSubscriptionRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
}

public sealed class DeleteAccountSubscriptionResponse(string message) :
    ResolvedData<object>(null, message);

