namespace IP.Core.Api.BusinessBranches.DeleteBusinessBranch;

public sealed record DeleteBusinessBranchCommand(Guid Id, DeleteBusinessBranchRequest Request) :
    ICommand<DeleteBusinessBranchResponse>, ILoggableData;

public sealed class DeleteBusinessBranchRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
}

public sealed class DeleteBusinessBranchResponse(string message) :
    ResolvedData<object>(null, message);
