namespace IP.AccIPInfo.Api.IntegratorSystems.GetSystem;

public sealed record GetSystemQuery(Guid Id) :
    IQuery<GetSystemResponse>;

public sealed class GetSystemResponse(
    Guid id,
    string name,
    string siteUrl,
    string description,
    bool active) :
    ResolvedData<GetSystemResponseData>(
        new GetSystemResponseData(id, name, siteUrl, description, active), string.Empty);

public sealed record GetSystemResponseData(
    Guid Id,
    string Name,
    string SiteUrl,
    string Description,
    bool Active);