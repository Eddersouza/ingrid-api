using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace IP.AccIPInfo.Api.AccountsIP.Get;

internal sealed class GetAccountsIPQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetAccountsIPQuery, GetAccountsIPResponse>
{
    private readonly IAccountIPRepository _employeeRepository =
        _unitOfWork.GetRepository<IAccountIPRepository>();

    public async Task<Result<GetAccountsIPResponse>> Handle(
        GetAccountsIPQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<AccountIP, object>>> sortDictionary = new()
        {   {"number", x => x.Number },
            {"alias", x => x.Alias!.Value! },
            {"customerName", x => x.Customer.Name},
            {"ownerName", x => x.Owner!.Name},
            {"retailerName", x => x.Retailer!.Name},
        };

        GetAccountsIPRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<AccountIP> queryAccount =
            _employeeRepository.Entities.AsNoTracking();

        IQueryable<AccountIPResponseData> accountsIP =
            ApplyUserFilters(queryRequest, queryAccount)
            .OrderBy(
                sortDictionary,
                query.Request.OrderBy, ["number"])
            .Paginate(pageNumber, pageSize)
            .Select(account => new AccountIPResponseData(account));

        int count = await queryAccount.CountAsync(cancellationToken);

        GetAccountsIPResponse response = new(
            accountsIP,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<AccountIP> ApplyUserFilters(
        GetAccountsIPRequest queryRequest,
        IQueryable<AccountIP> query)
    {
        string normalizedAlias =
            queryRequest?.AliasContains?.NormalizeCustom() ?? string.Empty;

        return query
            .WhereIf(queryRequest!.NumberContains is not null,
                query => EF.Functions.Like(query.Number.ToString(), queryRequest.NumberContains!.Value.ToString()))
            .WhereIf(normalizedAlias.IsNotNullOrWhiteSpace(),
                query => query.Alias!.ValueNormalized!.Contains(normalizedAlias))
            .WhereIf(queryRequest.BusinessBranchIs.HasValue, query =>
                query.BusinessBranchSegment!.BranchId == queryRequest.BusinessBranchIs!.Value)
            .WhereIf(queryRequest.BusinessSegmentIs.HasValue, query =>
                query.BusinessBranchSegment!.SegmentId == queryRequest.BusinessSegmentIs!.Value)
            .WhereIf(queryRequest.CustomerNameContains.IsNotNullOrWhiteSpace(),
                query => query.Customer.Name.Contains(queryRequest.CustomerNameContains!))
            .WhereIf(queryRequest.OwnerNameContains.IsNotNullOrWhiteSpace(),
                query => query.Owner!.Name.Contains(queryRequest.OwnerNameContains!))
            .WhereIf(queryRequest.RetailerNameContains.IsNotNullOrWhiteSpace(),
                query => query.Retailer!.Name.Contains(queryRequest.RetailerNameContains!))
            .WhereIf(queryRequest.StatusIs.HasValue, query =>
                query.StatusCode == queryRequest.StatusIs)
            .WhereIf(queryRequest.TypeIs.HasValue, query =>
                query.TypeCode == queryRequest.TypeIs);
    }
}