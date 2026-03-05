namespace IP.Core.Api.Employees.Get;

public sealed record GetEmployeesQuery(GetEmployeesRequest Request) :
    IQuery<GetEmployeesResponse>, ILoggableData;

public sealed class GetEmployeesRequest :
    QueryBaseFilter
{
    public string? NameContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'active', 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetEmployeesResponse(
    IEnumerable<EmployeeResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<EmployeeResponseData>(data, pagination);