namespace IP.Core.Api.BusinessSegments.GetSegments;

public sealed record GetBusinessSegmentsQuery(GetBusinessSegmentsQueryRequest Request) :
    IQuery<GetBusinessSegmentsResponse>;

public sealed class GetBusinessSegmentsQueryRequest :
    QueryBaseFilter,
    IQuery<GetBusinessSegmentsResponseData>
{
    public Guid? BusinessBranchId { get; set; }
    public string? SegmentNameContains { get; set; }
    public string? BusinessBranchContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'segmentName', 'businessBranch'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetBusinessSegmentsResponse(
    IEnumerable<GetBusinessSegmentsResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetBusinessSegmentsResponseData>(data, pagination);

public sealed record GetBusinessSegmentsResponseData(
    Guid Id, string BusinessBranch, string SegmentName, bool Active);
