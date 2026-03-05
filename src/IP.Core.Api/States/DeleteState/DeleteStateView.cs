namespace IP.Core.Api.States.DeleteState;

public sealed record DeleteStateCommand(Guid Id, DeleteStateRequest Request) :
    ICommand<DeleteStateResponse>, ILoggableData;

public sealed class DeleteStateRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
};

public sealed class DeleteStateResponse(string message) :
    ResolvedData<object>(null, message);