namespace IP.AccIPInfo.Api.Transactions.GetSummaryByOwner;

internal sealed class GetSummaryByOwnerQueryHandler(
    IAccIPUoW _unitOfWork,
    ITransactionSummaryService _transactionSummaryService) :
    IQueryHandler<GetSummaryByOwnerQuery, GetSummaryByOwnerResponse>
{
    private readonly IAccountMovementSummaryRepository _accountMovementSummaryRepository =
        _unitOfWork.GetRepository<IAccountMovementSummaryRepository>();

    public async Task<Result<GetSummaryByOwnerResponse>> Handle(
        GetSummaryByOwnerQuery query,
        CancellationToken cancellationToken)
    {  
        DateTime today = DateTime.Now;

        GetSummaryByOwnerRequest queryRequest = query.Request;

        Guid ownerId = query.OwnerId;       
        DateTime startDate = queryRequest.StartDate.FirstMomentOfMonth(today);
        DateTime endDate = queryRequest.EndDate.LastMomentOfMonth(today);
        IQueryable<AccountMovementSummary> queryAccount = await _transactionSummaryService.GetSummaryFiltered(
             ownerId,
             _accountMovementSummaryRepository.Entities
             .AsNoTracking(),
             queryRequest
         );

        var result = new GetSummaryByOwnerResponseData
        {
            TPV = queryAccount.Sum(x => x.SettledAmount),
            QuantityTransactions = queryAccount.Sum(x => x.SettledQuantity),
            QuantityCustomers = queryAccount.Select(x => x.AccountIP.Customer.Id).Distinct().Count(),
        };

        GetSummaryByOwnerResponse response = new(result);

        return Result.Success(response);
    }
}