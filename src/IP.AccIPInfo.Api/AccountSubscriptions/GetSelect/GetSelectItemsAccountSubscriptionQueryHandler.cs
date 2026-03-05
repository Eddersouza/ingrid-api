namespace IP.AccIPInfo.Api.AccountSubscriptions.GetSelect;

internal sealed class GetSelectItemsAccountSubscriptionQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsAccountSubscriptionQuery, GetSelectItemsAccountSubscriptionResponse>
{
    private readonly IAccountSubscriptionRepository _AccountSubscriptionRepository =
       _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<GetSelectItemsAccountSubscriptionResponse>> Handle(
        GetSelectItemsAccountSubscriptionQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AccountSubscription, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value! },
        };

        GetSelectItemsAccountSubscriptionQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<AccountSubscription> queryRoles =
            ApplyAccountSubscriptionFilters(searchTerm, _AccountSubscriptionRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["active", "name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name.Value,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsAccountSubscriptionResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AccountSubscription> ApplyAccountSubscriptionFilters(
        string searchTerm,
        IQueryable<AccountSubscription> queryRoles)
    {
        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();
        return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name.ValueNormalized.Contains(searchTermTrimmed));
    }
}