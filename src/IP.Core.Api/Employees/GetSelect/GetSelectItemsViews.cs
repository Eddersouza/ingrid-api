namespace IP.Core.Api.Employees.GetSelect;

public sealed record GetSelectItemsEmployeeQuery(GetSelectItemsEmployeeQueryRequest Request) :
    IQuery<GetSelectItemsEmployeeResponse>;

public sealed class GetSelectItemsEmployeeQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsEmployeeResponse>
{
    public string? SearchTerm { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetSelectItemsEmployeeResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);