namespace IP.Core.Api.BusinessBranches.GetBusinessBranches;

public sealed record GetBusinessBranchesQuery(GetBusinessBranchesQueryRequest Request) :
    IQuery<GetBusinessBranchesResponse>;

public sealed class GetBusinessBranchesQueryRequest :
    QueryBaseFilter,
    IQuery<GetBusinessBranchesResponseData>
{
    public string? NameContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}
public sealed class GetBusinessBranchesResponse(
    IEnumerable<GetBusinessBranchesResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetBusinessBranchesResponseData>(data, pagination);

public sealed record GetBusinessBranchesResponseData(
    Guid Id,
    string Name,
    bool Active);
