namespace IP.Core.Api.Cities.GetSelectItems;

public sealed record GetSelectItemsCitiesQuery(GetSelectItemsCitiesQueryRequest Request) :
    IQuery<GetSelectItemsCitiesResponse>;

public sealed class GetSelectItemsCitiesQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsCitiesResponse>
{
    public string? SearchTerm { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetSelectItemsCitiesResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);
