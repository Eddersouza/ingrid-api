namespace IP.Core.Api.BusinessSegments.GetSelect;

public sealed record GetSelectItemsBusinessSegmentQuery(GetSelectItemsBusinessSegmentQueryRequest Request) :
    IQuery<GetSelectItemsBusinessSegmentResponse>;

public sealed class GetSelectItemsBusinessSegmentQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsBusinessSegmentResponse>
{
    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }

    [Description(@"Termo de pesquisa, se quiser buscar por segments vinculados a uma branch passe o id da branch")]
    public string? SearchTerm { get; set; }
}

public sealed class GetSelectItemsBusinessSegmentResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);