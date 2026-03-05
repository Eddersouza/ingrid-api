namespace IP.Core.Api.BusinessBranches.ActiveBusinessBranch;

public sealed record ActiveBusinessBranchCommand(Guid Id, ActiveBusinessBranchRequest Request) :
    ICommand<ActiveBusinessBranchResponse>, ILoggableData;

public sealed class ActiveBusinessBranchRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;

}

public sealed class ActiveBusinessBranchResponse(Guid id, string name, bool active, string message) :
    ResolvedData<ActiveBusinessBranchResponseData>(
        new ActiveBusinessBranchResponseData(id, name, active), message);

public sealed record ActiveBusinessBranchResponseData(Guid Id, string Name, bool Active);
