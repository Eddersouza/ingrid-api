namespace IP.AccIPInfo.Api.AccountSubscriptions.GetAccountSubscription;

internal sealed class GetAccountSubscriptionQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetAccountSubscriptionQuery, GetAccountSubscriptionResponse>
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
        _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<GetAccountSubscriptionResponse>> Handle(
        GetAccountSubscriptionQuery query,
        CancellationToken cancellationToken)
    {
        AccountSubscriptionId id = new(query.Id);
        AccountSubscription? currentAccountSubscription =
            await _accountSubscriptionRepository.Data()
            .AsNoTracking()
            .SingleAsync(s => s.Id == id, cancellationToken);

        if (currentAccountSubscription is null) return AccountSubscriptionErrors.AccountSubscriptionNotFound;

        GetAccountSubscriptionResponse response = new(
            currentAccountSubscription.Id.Value!,
            currentAccountSubscription.Name.Value!,
            currentAccountSubscription.ActivableInfo.Active);

        return Result.Success(response);
    }
}