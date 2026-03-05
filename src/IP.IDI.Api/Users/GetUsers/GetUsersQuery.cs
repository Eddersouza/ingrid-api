using System.ComponentModel;

namespace IP.IDI.Api.Users.GetUsers;

public sealed record GetUsersQuery(GetUsersQueryRequest Request) :
    IQuery<GetUsersResponse>;

public sealed class GetUsersQueryRequest :
    QueryBaseFilter,
    IQuery<GetUsersResponse>
{
    public string? UserContains { get; set; }
    public string? EmailContains { get; set; }

    public Guid? RoleIs { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name', email', 'role'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetUsersResponse(
    IEnumerable<UserResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<UserResponseData>(data, pagination);