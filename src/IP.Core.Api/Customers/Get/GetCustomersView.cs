namespace IP.Core.Api.Customers.Get;

public sealed record GetCustomersQuery(GetCustomersRequest Request) :
    IQuery<GetCustomersResponse>, ILoggableData;

public sealed class GetCustomersRequest :
    QueryBaseFilter
{
    public string? NameContains { get; set; }
    public string? TradingNameContains { get; set; }

    public PersonTypeEnum? PersonTypeIs { get; set; }

    public CustomerStatusEnum? StatusIs { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetCustomersResponse(
    IEnumerable<CustomerResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<CustomerResponseData>(data, pagination);