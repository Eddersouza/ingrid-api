namespace IP.AccIPInfo.Api.AccountSubscriptions.GetSelect;

public sealed record GetSelectItemsAccountSubscriptionQuery(GetSelectItemsAccountSubscriptionQueryRequest Request) :
    IQuery<GetSelectItemsAccountSubscriptionResponse>;

public sealed class GetSelectItemsAccountSubscriptionQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsAccountSubscriptionResponse>
{
    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }

    public string? SearchTerm { get; set; }
}

public sealed class GetSelectItemsAccountSubscriptionResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);