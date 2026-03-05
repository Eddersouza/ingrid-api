namespace IP.Core.Api.BusinessSegments.Create;

public sealed record CreateBusinessSegmentCommand(CreateBusinessSegmentRequest Request) :
    ICommand<CreateBusinessSegmentResponse>, ILoggableData;
public sealed class CreateBusinessSegmentRequest
{
    [Required]
    [MinLength(BusinessSegment.SEGMENT_NAME_MIN_LENGTH)]
    [MaxLength(BusinessSegment.SEGMENT_NAME_MAX_LENGTH)]
    public string SegmentName { get; set; } = string.Empty;

    [Required]
    public Guid BusinessBranchId { get; set; } = default!;
}

public sealed class CreateBusinessSegmentResponse(
    Guid id,
    BusinessBranchId businessBranchId,
    string segmentName,
    string message,
    bool active) :
    ResolvedData<CreateBusinessSegmentResponseData>(
        new CreateBusinessSegmentResponseData(id, businessBranchId.Value, segmentName, active), message);

public sealed record CreateBusinessSegmentResponseData(Guid Id, Guid BusinessBranchId, string SegmentName, bool Active);
