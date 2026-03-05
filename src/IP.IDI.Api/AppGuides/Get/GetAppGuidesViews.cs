using System.ComponentModel;

namespace IP.IDI.Api.AppGuides.Get;
public sealed record GetAppGuidesQuery(GetAppGuidesQueryRequest Request) :
    IQuery<GetAppGuidesResponse>, ILoggableData;

public sealed class GetAppGuidesQueryRequest :
    QueryBaseFilter,
    IQuery<GetAppGuidesResponse>
{
    public string? NameContains { get; set; }
    public string? LinkIdContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name', linkId'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetAppGuidesResponse(
    IEnumerable<GetAppGuidesResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetAppGuidesResponseData>(data, pagination);

public sealed record GetAppGuidesResponseData(Guid Id, string Name, string LinkId, int ViewCount, int TotalUsers, bool Active);