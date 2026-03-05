namespace IP.AccIPInfo.Api.IntegratorSystems.DeleteSystem;

public sealed record DeleteSystemCommand(Guid Id, DeleteSystemRequest Request) :
    ICommand<DeleteSystemResponse>, ILoggableData;

public sealed class DeleteSystemRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
}

public sealed class DeleteSystemResponse(string message) :
    ResolvedData<object>(null, message);