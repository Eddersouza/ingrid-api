namespace IP.Core.Api.States.GetStates;

public sealed record GetStatesQuery(GetStatesQueryRequest Request) :
    IQuery<GetStatesResponse>;

public sealed class GetStatesQueryRequest : 
    QueryBaseFilter, 
    IQuery<GetStatesResponseData>
{
    public string? IBGECodeContains { get; set; }
    public string? CodeContains { get; set; }
    public string? NameContains { get; set; }

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'name'")]
    public string[]? OrderBy { get; set; }
}


public sealed class GetStatesResponse(
    IEnumerable<GetStatesResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<GetStatesResponseData>(data, pagination);

public sealed record GetStatesResponseData(
    Guid Id, string IBGECode, string Code, string Name);
