namespace IP.AccIPInfo.Api.IntegratorSystems.GetSystems;

public sealed record GetSystemsQuery(GetSystemsQueryRequest Request) :
    IQuery<GetSystemsResponse>;

public sealed class GetSystemsQueryRequest :
    QueryBaseFilter,
    IQuery<GetSystemsResponseData>
{
    public string? NameContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name', 'siteUrl', 'description'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetSystemsResponse(
    IEnumerable<GetSystemsResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetSystemsResponseData>(data, pagination);

public sealed record GetSystemsResponseData(
    Guid Id,
    string Name,
    string SiteUrl,
    string Description,
    bool Active);