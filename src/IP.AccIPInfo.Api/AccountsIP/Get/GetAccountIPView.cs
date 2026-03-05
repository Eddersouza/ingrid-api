namespace IP.AccIPInfo.Api.AccountsIP.Get;

public sealed record GetAccountsIPQuery(GetAccountsIPRequest Request) :
    IQuery<GetAccountsIPResponse>, ILoggableData;

public sealed class GetAccountsIPRequest :
    QueryBaseFilter
{
    public int? NumberContains { get; set; }
    public string? AliasContains { get; set; }
    public Guid? BusinessBranchIs { get; set; }
    public Guid? BusinessSegmentIs { get; set; }
    public string? CustomerNameContains { get; set; }
    public string? OwnerNameContains { get; set; }
    public string? RetailerNameContains { get; set; }
    public AccountIPStatus? StatusIs { get; set; }
    public AccountIPType? TypeIs { get; set; }



    [Description(@"Ordenação dos Itens.
Por padrão: teremos o nome do campo e a direção (asc ou desc) separados por dois pontos.
Ex.: 'name:asc'
Caso deixe apenas os nomes dos campos a ordenação será ascendente.
Valores possiveis: 'number', 'alias', 'customerName', 'ownerName', 'retailerName'")]
    public string[]? OrderBy { get; set; }
}

public sealed class GetAccountsIPResponse(
    IEnumerable<AccountIPResponseData> data,
    ResolvedDataPagination pagination) :
    ResolvedDataPaginated<AccountIPResponseData>(data, pagination);