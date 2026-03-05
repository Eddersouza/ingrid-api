namespace IP.AccIPInfo.Api.AccountSubscriptions.GetAccountSubscriptions;

public sealed record GetAccountSubscriptionsQuery(GetAccountSubscriptionsQueryRequest Request) :
    IQuery<GetAccountSubscriptionsResponse>;

public sealed class GetAccountSubscriptionsQueryRequest :
    QueryBaseFilter,
    IQuery<GetAccountSubscriptionsResponseData>
{
    public string? NameContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetAccountSubscriptionsResponse(
    IEnumerable<GetAccountSubscriptionsResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetAccountSubscriptionsResponseData>(data, pagination);

public sealed record GetAccountSubscriptionsResponseData(
    Guid Id, string Name, bool Active);

