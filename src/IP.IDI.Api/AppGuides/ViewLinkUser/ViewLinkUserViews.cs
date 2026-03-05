using System.ComponentModel;

namespace IP.IDI.Api.AppGuides.ViewLinkUser;

public sealed record ViewLinkUserQuery(string LinkId, ViewLinkUserQueryRequest Request) :
    IQuery<ViewLinkUserResponse>;

public sealed class ViewLinkUserQueryRequest :
    QueryBaseFilter,
    IQuery<ViewLinkUserResponse>
{
    public string? UserContains { get; set; }

    public string? EmailContains { get; set; }
    

    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'name'")]
    public string[]? OrderBy { get; set; }
}

public sealed class ViewLinkUserResponse(
    IEnumerable<ViewLinkUserResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<ViewLinkUserResponseData>(data, pagination);

public sealed record ViewLinkUserResponseData(Guid Id, string User, string Email, IEnumerable<string> ViewedDates, bool Active );