namespace IP.AccIPInfo.Api.Transactions.GetCustomerOnSummary;

public sealed record GetCustomerOnSummaryQuery(GetCustomerOnSummaryQueryRequest Request) :
    IQuery<GetCustomerOnSummaryResponse>;

public sealed class GetCustomerOnSummaryQueryRequest :
    QueryBaseFilter,
    IQuery<GetCustomerOnSummaryResponse>
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? SearchTerm { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetCustomerOnSummaryResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);