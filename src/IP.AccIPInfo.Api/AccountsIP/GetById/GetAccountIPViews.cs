namespace IP.AccIPInfo.Api.AccountsIP.GetById;

public sealed record GetAccountIPQuery(Guid Id) :
    IQuery<GetAccountIPResponse>;

public sealed class GetAccountIPResponse(
    AccountIP AccountIP) :
    ResolvedData<AccountIPResponseData>(
        new AccountIPResponseData(AccountIP), string.Empty);