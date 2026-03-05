namespace IP.AccIPInfo.Api.AccountSubscriptions.UpdateAccountSubscription;

internal sealed record UpdateAccountSubscriptionCommand(Guid Id, UpdateAccountSubscriptionRequest Request) :
    ICommand<UpdateAccountSubscriptionResponse>, ILoggableData;

public class UpdateAccountSubscriptionRequest
{
    [Required]
    [MinLength(AccountSubscription.NAME_MIN_LENGTH)]
    [MaxLength(AccountSubscription.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;
}
public sealed class UpdateAccountSubscriptionResponse(
    Guid id,
    string name,
    string message) :
    ResolvedData<UpdateAccountSubscriptionResponseData>(
        new UpdateAccountSubscriptionResponseData(id, name), message)
{ };

public sealed record UpdateAccountSubscriptionResponseData(
    Guid Id, string Name);

