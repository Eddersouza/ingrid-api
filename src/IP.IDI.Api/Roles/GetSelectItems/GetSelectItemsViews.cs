using System.ComponentModel;

namespace IP.IDI.Api.Roles.GetSelectItems;

public sealed record GetSelectItemsRolesQuery(GetSelectItemsRolesQueryRequest Request) :
    IQuery<GetSelectItemsRolesResponse>;

public sealed class GetSelectItemsRolesQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsRolesResponse>
{
    public string? SearchTerm { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetSelectItemsRolesResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);