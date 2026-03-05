namespace IP.AccIPInfo.Api.Transactions.SummaryAccountsByOwner;

internal sealed class GetSummaryAccountsByOwnerQueryHandler(
    IAccIPUoW _unitOfWork,
    ITransactionSummaryService _transactionSummaryService) :
    IQueryHandler<GetSummaryAccountsByOwnerQuery, GetSummaryAccountsByOwnerResponse>
{
    private readonly IAccountMovementSummaryRepository _accountMovementSummaryRepository =
        _unitOfWork.GetRepository<IAccountMovementSummaryRepository>();

    public async Task<Result<GetSummaryAccountsByOwnerResponse>> Handle(
        GetSummaryAccountsByOwnerQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<GetSummaryAccountsByOwnerResponseData, object>>> sortDictionary = new()
        {
            {"accountNumber", x => x.AccountNumber },
            {"customerName", x => x.CustomerName },
            {"systemName", x => x.SystemName },
            {"retailName", x => x.RetailerName },
            {"ownerName", x => x.OwnerName },
            {"settledAmount", x => x.SettledAmount },
            {"settledQuantity", x => x.SettledQuantity },
            {"cpt", x => x.AverageTicketPrice }
        };

        GetSummaryAccountsByOwnerRequest queryRequest = query.Request;

        Guid ownerId = query.OwnerId;
        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 1000000;

        IQueryable<AccountMovementSummary> queryAccount = await _transactionSummaryService.GetSummaryFiltered(
            ownerId,
            _accountMovementSummaryRepository.Entities
            .AsNoTracking(),
            queryRequest
        );

        var grouped = queryAccount.GroupBy(x => new
        {
            x.AccountNumber,
            OwnerId = x.AccountIP.Owner != null ? x.AccountIP.Owner.Id : null,
            OwnerName = x.AccountIP.Owner != null ? x.AccountIP.Owner.Name : null,
            OwnerInternal = x.AccountIP.Owner != null ? x.AccountIP.Owner.OwnerIsIP : null,
            RetailerId = x.AccountIP.Retailer != null ? x.AccountIP.Retailer.Id : null,
            RetailerName = x.AccountIP.Retailer != null ? x.AccountIP.Retailer.Name : null,
            SystemId = x.AccountIP.Integrator != null ? x.AccountIP.Integrator.Id : null,
            SystemName = x.AccountIP.Integrator != null ? x.AccountIP.Integrator.Name : null,
            CustomerId = x.AccountIP.Customer.Id,
            CustomerName = x.AccountIP.Customer.Name
        });

        var newValues = grouped.Select(g => new GetSummaryAccountsByOwnerResponseData
        {
            AccountNumber = g.Key.AccountNumber,
            OwnerId = g.Key.OwnerId,
            OwnerName =  g.Key.OwnerInternal == true ? "Interno" : g.Key.OwnerName ?? string.Empty,
            RetailerId = g.Key.RetailerId,
            RetailerName = g.Key.RetailerName ?? string.Empty,
            SystemId = g.Key.SystemId,
            SystemName = g.Key.SystemName ?? string.Empty,
            CustomerId = g.Key.CustomerId!.Value,
            CustomerName = g.Key.CustomerName ?? string.Empty,
            SettledAmount = g.Sum(x => x.SettledAmount),
            SettledQuantity = g.Sum(x => x.SettledQuantity),
            AverageTicketPrice =
            g.Sum(x => x.SettledQuantity) == 0
                ? 0
                : g.Sum(x => x.SettledAmount) /
                g.Sum(x => x.SettledQuantity)
        }).OrderBy(
            sortDictionary,
            query.Request.OrderBy, ["settledAmount:desc"]).
            Paginate(pageNumber, pageSize);

        int count = await newValues.CountAsync(cancellationToken);

        List<GetSummaryAccountsByOwnerResponseData> accountMovements = [.. newValues];

        GetSummaryAccountsByOwnerResponse response = new(
            accountMovements,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
}