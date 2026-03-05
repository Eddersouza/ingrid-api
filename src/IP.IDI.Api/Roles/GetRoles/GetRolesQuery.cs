using System.ComponentModel;

namespace IP.IDI.Api.Roles.GetRoles;

public sealed record GetRolesQuery(GetRolesQueryRequest Request) :
    IQuery<GetRolesResponse>, ILoggableData;

public sealed class GetRolesQueryRequest :
    QueryBaseFilter,
    IQuery<GetRolesResponse>
{
    public string? NameContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetRolesResponse(
    IEnumerable<GetRolesResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetRolesResponseData>(data, pagination);

public sealed record GetRolesResponseData(Guid Id, string Name, bool Active);