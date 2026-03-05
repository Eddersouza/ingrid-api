namespace IP.Core.Api.BusinessSegments.DeleteSegment;

public sealed record DeleteBusinessSegmentCommand(Guid Id, DeleteBusinessSegmentRequest Request) :
    ICommand<DeleteBusinessSegmentResponse>, ILoggableData;

public sealed class DeleteBusinessSegmentRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
}

public sealed class DeleteBusinessSegmentResponse(string message) :
    ResolvedData<object>(null, message);

