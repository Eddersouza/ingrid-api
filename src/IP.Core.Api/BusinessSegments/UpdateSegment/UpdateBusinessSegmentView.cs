namespace IP.Core.Api.BusinessSegments.UpdateSegment;

internal sealed record UpdateBusinessSegmentCommand(Guid Id, UpdateBusinessSegmentRequest Request) :
    ICommand<UpdateBusinessSegmentResponse>, ILoggableData;

public class UpdateBusinessSegmentRequest
{
    [Required]
    [MinLength(BusinessSegment.SEGMENT_NAME_MIN_LENGTH)]
    [MaxLength(BusinessSegment.SEGMENT_NAME_MAX_LENGTH)]
    public string SegmentName { get; set; } = string.Empty;
}
public sealed class UpdateBusinessSegmentResponse(
    Guid id,
    string segmentName,
    string message) :
    ResolvedData<UpdateBusinessSegmentResponseData>(
        new UpdateBusinessSegmentResponseData(id, segmentName), message)
{ };

public sealed record UpdateBusinessSegmentResponseData(
    Guid Id, string SegmentName);

