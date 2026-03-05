namespace IP.Core.Api.Cities.GetCities;

public sealed record GetCitiesQuery(GetCitiesQueryRequest Request) :
    IQuery<GetCitiesResponse>;

public sealed class GetCitiesQueryRequest :
    QueryBaseFilter,
    IQuery<GetCitiesResponseData>
{
    public string? IBGECodeContains { get; set; }
    public string? NameContains { get; set; }
    public string? StateContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name', 'stateName'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetCitiesResponse(
    IEnumerable<GetCitiesResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetCitiesResponseData>(data, pagination);

public sealed record GetCitiesResponseData(
    Guid Id, string IBGECode, string Name, string StateName, bool Active);
