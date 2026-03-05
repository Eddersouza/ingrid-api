namespace IP.Core.Api.BusinessSegments.ActiveSegment;

public sealed record ActiveBusinessSegmentCommand(Guid Id, ActiveBusinessSegmentRequest Request) :
    ICommand<ActiveBusinessSegmentResponse>, ILoggableData;

public sealed class ActiveBusinessSegmentRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;

}

public sealed class ActiveBusinessSegmentResponse(Guid id, string segmentName, bool active, string message) :
    ResolvedData<ActiveBusinessSegmentResponseData>(
        new ActiveBusinessSegmentResponseData(id, segmentName, active), message);

public sealed record ActiveBusinessSegmentResponseData(Guid Id, string SegmentName, bool Active);
