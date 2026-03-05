namespace IP.AccIPInfo.Api.AccountSubscriptions.CreateAccountSubscription;

public sealed record CreateAccountSubscriptionCommand(CreateAccountSubscriptionRequest Request) :
    ICommand<CreateAccountSubscriptionResponse>, ILoggableData;

public sealed class CreateAccountSubscriptionRequest
{
    [Required]
    [MinLength(AccountSubscription.NAME_MIN_LENGTH)]
    [MaxLength(AccountSubscription.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(AccountSubscription.EXTERNAL_ID_MAX_LENGTH)]
    public string? ExternalId { get; set; }
}

public sealed class CreateAccountSubscriptionResponse(
    Guid id,
    string name,
    string message,
    bool active,
    string? externalId) :
    ResolvedData<CreateAccountSubscriptionResponseData>(
        new CreateAccountSubscriptionResponseData(id, name, active, externalId), message);

public sealed record CreateAccountSubscriptionResponseData(
    Guid Id,
    string Name,
    bool Active,
    string? ExternalId);