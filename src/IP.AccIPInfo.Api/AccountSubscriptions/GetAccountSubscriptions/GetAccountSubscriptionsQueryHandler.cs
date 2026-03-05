namespace IP.AccIPInfo.Api.AccountSubscriptions.GetAccountSubscriptions;
internal class GetAccountSubscriptionsQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetAccountSubscriptionsQuery, GetAccountSubscriptionsResponse>
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
        _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<GetAccountSubscriptionsResponse>> Handle(
        GetAccountSubscriptionsQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AccountSubscription, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value }
        };

        GetAccountSubscriptionsQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<AccountSubscription> queryAccountSubscriptions =
            _accountSubscriptionRepository.Data().AsNoTracking();

        IQueryable<GetAccountSubscriptionsResponseData> accountSubscription =
            ApplyUserFilters(queryRequest, queryAccountSubscriptions)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetAccountSubscriptionsResponseData(
                x.Id.Value,
                x.Name.Value,
                x.ActivableInfo.Active));

        int count = await queryAccountSubscriptions.CountAsync(cancellationToken);

        GetAccountSubscriptionsResponse response = new(
            accountSubscription,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);

    }
    private static IQueryable<AccountSubscription> ApplyUserFilters(
        GetAccountSubscriptionsQueryRequest queryRequest,
        IQueryable<AccountSubscription> queryAccountSubscriptions)
    {
        string normalizedName = queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;

        return queryAccountSubscriptions
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.Name.ValueNormalized.Contains(normalizedName) );
    }

}
