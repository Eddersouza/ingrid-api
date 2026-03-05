namespace IP.AccIPInfo.Api.AccountSubscriptions.GetAccountSubscription;

public sealed record GetAccountSubscriptionQuery(Guid Id) :
    IQuery<GetAccountSubscriptionResponse>;

public sealed class GetAccountSubscriptionResponse(
        Guid id, string name, bool active) :
    ResolvedData<GetAccountSubscriptionResponseData>(
        new GetAccountSubscriptionResponseData(id, name, active), string.Empty);

public sealed record GetAccountSubscriptionResponseData(
    Guid Id, string Name, bool Active);
