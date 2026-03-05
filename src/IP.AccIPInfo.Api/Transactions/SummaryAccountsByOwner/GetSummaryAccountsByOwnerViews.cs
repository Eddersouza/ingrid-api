namespace IP.AccIPInfo.Api.Transactions.SummaryAccountsByOwner;

public sealed record GetSummaryAccountsByOwnerQuery(Guid OwnerId, GetSummaryAccountsByOwnerRequest Request) :
    IQuery<GetSummaryAccountsByOwnerResponse>;

public sealed class GetSummaryAccountsByOwnerRequest :
    QueryBaseFilter,
    IQuery<GetSummaryAccountsByOwnerResponseData>, ITransactionSummaryConditions
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? CustomerId { get; set; }
    public Guid? OwnerFilterId { get; set; }
    public bool? OwnerIsIP { get; set; }
    public Guid? RetailerId { get; set; }
    public Guid? IntegratorId { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'accountNumber', 'customerName', 'systemName', 'retailName', 'ownerName', 'settledAmount', 'settledQuantity', 'cpt'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetSummaryAccountsByOwnerResponse(
    IEnumerable<GetSummaryAccountsByOwnerResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetSummaryAccountsByOwnerResponseData>(data, pagination);

public sealed class GetSummaryAccountsByOwnerResponseData
{
    public int AccountNumber { get; init; } = default;
    public Guid? SystemId { get; init; } = default;
    public string SystemName { get; init; } = string.Empty;
    public Guid? RetailerId { get; init; } = default;
    public string RetailerName { get; init; } = string.Empty;
    public Guid? OwnerId { get; init; } = default;
    public string OwnerName { get; init; } = string.Empty;
    public Guid CustomerId { get; init; } = default;
    public string CustomerName { get; init; } = string.Empty;

    public decimal SettledAmount { get; set; } = default;
    public int SettledQuantity { get; set; } = default;

    public decimal AverageTicketPrice { get; set; } = default;
}