namespace IP.AccIPInfo.Api.Transactions.GetOwnerOnSummary;

internal sealed class GetOwnerOnSummaryQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetOwnerOnSummaryQuery, GetOwnerOnSummaryResponse>
{
    private readonly IAccountMovementSummaryRepository _accountMovementSummaryRepository =
        _unitOfWork.GetRepository<IAccountMovementSummaryRepository>();

    public async Task<Result<GetOwnerOnSummaryResponse>> Handle(
        GetOwnerOnSummaryQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<ValueLabelItem, object>>> sortDictionary = new()
        {
            { "active", x => !x.Disabled },
            { "name", x => x.Label! },
        };

        GetOwnerOnSummaryQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;
        DateTime today = DateTime.Now;
        DateTime fromDate = queryRequest.FromDate ?? new DateTime(today.Year, today.Month, 1);
        DateTime toDate = queryRequest.ToDate ?? fromDate.AddMonths(1).AddTicks(-1);

        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();

        IQueryable<AccountMovementSummary> queryAccount =
            _accountMovementSummaryRepository.Entities
            .AsNoTracking()
            .Where(x=> x.AccountIP.Owner != null && x.AccountIP.Owner.Name != null)
            .WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
                query => query.AccountIP.Owner!.Name.Contains(searchTermTrimmed));

        var grouped = queryAccount.GroupBy(x => new
        {
            x.AccountIP.Owner!.Id,
            x.AccountIP.Owner.Name
        }).OrderBy(account => account.Key.Name);

        var result = grouped.Paginate(pageNumber, pageSize)
        .Select(x => new ValueLabelItem(
            x.Key.Id!.Value.ToString(),
            x.Key.Name,
            false));

        int count = await grouped.CountAsync(cancellationToken);

        GetOwnerOnSummaryResponse response = new(
            result,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
}