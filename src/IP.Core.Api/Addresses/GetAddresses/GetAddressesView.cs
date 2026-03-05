namespace IP.Core.Api.Addresses.GetAddresses;

public sealed record GetAddressesQuery(GetAddressesQueryRequest Request) :
    IQuery<GetAddressesResponse>;

public sealed class GetAddressesQueryRequest :
    QueryBaseFilter,
    IQuery<GetAddressesResponseData>
{
    public Guid? CityId { get; set; }
    public string? NameContains { get; set; }
    public string? CodeContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'segmentName', 'businessBranch'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetAddressesResponse(
    IEnumerable<GetAddressesResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetAddressesResponseData>(data, pagination);

public sealed record GetAddressesResponseData(
    Guid Id, GetAddressCityData City, string Neighborhood, string StateCode, string Name, string Code, bool Active);

public sealed record GetAddressCityData(string Id, string Name);