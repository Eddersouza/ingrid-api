namespace IP.Core.Api.Customers.GetSelect;

public sealed record GetSelectItemsCustomerQuery(GetSelectItemsCustomerQueryRequest Request) :
    IQuery<GetSelectItemsCustomerResponse>;

public sealed class GetSelectItemsCustomerQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsCustomerResponse>
{
    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }

    public string? SearchTerm { get; set; }
}

public sealed class GetSelectItemsCustomerResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);