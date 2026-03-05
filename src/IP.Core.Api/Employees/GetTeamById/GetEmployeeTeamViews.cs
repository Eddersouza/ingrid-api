namespace IP.Core.Api.Employees.GetTeamById;

public sealed record GetEmployeeTeamQuery(Guid Id, GetEmployeeTeamQueryRequest Request) :
    IQuery<GetEmployeeTeamResponse>;

public sealed class GetEmployeeTeamQueryRequest :
    QueryBaseFilter,
    IQuery<GetEmployeeTeamResponse>
{
    public string? SearchTerm { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetEmployeeTeamResponse(
    IEnumerable<ValueLabelItem> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ValueLabelItem>(data, pagination);