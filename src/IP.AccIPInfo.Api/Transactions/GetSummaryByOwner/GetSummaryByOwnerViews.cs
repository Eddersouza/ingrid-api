namespace IP.AccIPInfo.Api.Transactions.GetSummaryByOwner;

public sealed record GetSummaryByOwnerQuery(Guid OwnerId, GetSummaryByOwnerRequest Request) :
    IQuery<GetSummaryByOwnerResponse>;

public sealed class GetSummaryByOwnerRequest :
    IQuery<GetSummaryByOwnerResponseData>, ITransactionSummaryConditions
{
    public DateTime? EndDate { get; set; }
    public DateTime? StartDate { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? OwnerFilterId { get; set; }
    public bool? OwnerIsIP { get; set; }
    public Guid? RetailerId { get; set; }
    public Guid? IntegratorId { get; set; }
}

public sealed class GetSummaryByOwnerResponse(
    GetSummaryByOwnerResponseData data) :
    ResolvedData<GetSummaryByOwnerResponseData>(
        data, string.Empty);

public sealed class GetSummaryByOwnerResponseData
{
    public decimal QuantityCustomers { get; set; }
    public int QuantityTransactions { get; set; }
    public decimal TPV { get; set; }
}