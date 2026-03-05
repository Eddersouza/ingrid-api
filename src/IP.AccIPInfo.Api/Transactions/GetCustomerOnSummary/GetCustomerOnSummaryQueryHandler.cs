namespace IP.AccIPInfo.Api.Transactions.GetCustomerOnSummary;

internal sealed class GetCustomerOnSummaryQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetCustomerOnSummaryQuery, GetCustomerOnSummaryResponse>
{
    private readonly IAccountMovementSummaryRepository _accountMovementSummaryRepository =
        _unitOfWork.GetRepository<IAccountMovementSummaryRepository>();

    public async Task<Result<GetCustomerOnSummaryResponse>> Handle(
        GetCustomerOnSummaryQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<ValueLabelItem, object>>> sortDictionary = new()
        {
            { "active", x => !x.Disabled },
            { "name", x => x.Label! },
        };

        GetCustomerOnSummaryQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;
        DateTime today = DateTime.Now;
        DateTime  fromDate = queryRequest.FromDate ?? new DateTime(today.Year, today.Month, 1);
        DateTime  toDate = queryRequest.ToDate ?? fromDate.AddMonths(1).AddTicks(-1);

        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();

        IQueryable<AccountMovementSummary> queryAccount =
            _accountMovementSummaryRepository.Entities
            .AsNoTracking()
            .WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
                query => query.AccountIP.Customer.Name.Contains(searchTermTrimmed));

        var grouped = queryAccount.GroupBy(x => new
        {
            CustomerId = x.AccountIP.Customer.Id,
            CustomerName = x.AccountIP.Customer.Name
        }).OrderBy(account => account.Key.CustomerName);

        var result = grouped.Paginate(pageNumber, pageSize)
        .Select(x => new ValueLabelItem(
                x.Key.CustomerId!.Value.ToString(),
                x.Key.CustomerName,
                false));

        int count = await grouped.CountAsync(cancellationToken);

        GetCustomerOnSummaryResponse response = new(
            result,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
}