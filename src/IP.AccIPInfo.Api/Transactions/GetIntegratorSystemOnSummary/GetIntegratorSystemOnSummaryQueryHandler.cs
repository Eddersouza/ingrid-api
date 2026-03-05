namespace IP.AccIPInfo.Api.Transactions.GetIntegratorSystemOnSummary;

internal sealed class GetIntegratorSystemOnSummaryQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetIntegratorSystemOnSummaryQuery, GetIntegratorSystemOnSummaryResponse>
{
    private readonly IAccountMovementSummaryRepository _accountMovementSummaryRepository =
        _unitOfWork.GetRepository<IAccountMovementSummaryRepository>();

    public async Task<Result<GetIntegratorSystemOnSummaryResponse>> Handle(
        GetIntegratorSystemOnSummaryQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<ValueLabelItem, object>>> sortDictionary = new()
        {
            { "active", x => !x.Disabled },
            { "name", x => x.Label! },
        };

        GetIntegratorSystemOnSummaryQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;
        DateTime today = DateTime.Now;
        DateTime fromDate = queryRequest.FromDate.FirstMomentOfMonth(today);
        DateTime toDate = queryRequest.ToDate.LastMomentOfMonth(today);

        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();

        IQueryable<AccountMovementSummary> queryAccount =
            _accountMovementSummaryRepository.Entities
            .AsNoTracking()
            .Where(x => x.AccountIP.Integrator != null && x.AccountIP.Integrator.Name != null)
            .WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
                query => query.AccountIP.Integrator!.Name.Contains(searchTermTrimmed));

        var grouped = queryAccount.GroupBy(x => new
        {
            x.AccountIP.Integrator!.Id,
            x.AccountIP.Integrator.Name
        }).OrderBy(account => account.Key.Name);

        var result = grouped.Paginate(pageNumber, pageSize)
        .Select(x => new ValueLabelItem(
                x.Key.Id!.Value.ToString(),
                x.Key.Name,
                false));

        int count = await grouped.CountAsync(cancellationToken);

        GetIntegratorSystemOnSummaryResponse response = new(
            result,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
}