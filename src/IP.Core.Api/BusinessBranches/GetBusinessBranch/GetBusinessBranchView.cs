namespace IP.Core.Api.BusinessBranches.GetBusinessBranch;

public sealed record GetBusinessBranchQuery(Guid Id) :
    IQuery<GetBusinessBranchResponse>;

public sealed class GetBusinessBranchResponse(
    Guid id,
    string name,
    bool active) :
    ResolvedData<GetBusinessBranchResponseData>(
        new GetBusinessBranchResponseData(id, name, active), string.Empty);

public sealed record GetBusinessBranchResponseData(
    Guid Id,
    string Name,
    bool Active);
