using System.ComponentModel;

namespace IP.IDI.Api.Users.GetSelect;

public sealed record GetSelectItemsUserQuery(GetSelectItemsUserQueryRequest Request) :
    IQuery<GetSelectItemsUserResponse>;

public sealed class GetSelectItemsUserQueryRequest :
    QueryBaseFilter,
    IQuery<GetSelectItemsUserResponse>
{
    public string? SearchTerm { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetSelectItemsUserResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);