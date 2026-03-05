namespace IP.Core.Api.BusinessSegments.GetSegment;

public sealed record GetBusinessSegmentQuery(Guid Id) :
    IQuery<GetBusinessSegmentResponse>;

public sealed class GetBusinessSegmentResponse(
        Guid id, string segmentName, Guid businessBranchId, string businessBranchName, bool active) :
    ResolvedData<GetBusinessSegmentResponseData>(
        new GetBusinessSegmentResponseData(id, segmentName, businessBranchId, businessBranchName, active), string.Empty);

public sealed record GetBusinessSegmentResponseData(
    Guid Id, string SegmentName, Guid BusinessBranchId, string BusinessBranchName, bool Active);
