namespace IP.IDI.Api.AppGuides.GetById;

public sealed record GetAppGuideQuery(Guid Id) :
    IQuery<GetAppGuideResponse>, ILoggableData;

public sealed class GetAppGuideResponse(
    GetAppGuideResponseData data) :
    ResolvedData<GetAppGuideResponseData>(data, string.Empty);

public sealed record GetAppGuideResponseData(
    Guid Id, string Name, string LinkId, int ViewCount, int TotalUsers, bool Active);