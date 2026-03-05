namespace IP.Core.Api.BusinessBranches.Create;

public sealed record CreateBusinessBranchCommand(CreateBusinessBranchRequest Request) :
    ICommand<CreateBusinessBranchResponse>, ILoggableData;
public sealed class CreateBusinessBranchRequest
{
    [Required]
    [MinLength(BusinessBranch.NAME_MIN_LENGTH)]
    [MaxLength(BusinessBranch.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;
}

public sealed class CreateBusinessBranchResponse(
    Guid id,
    string name,
    string message,
    bool active) :
    ResolvedData<CreateBusinessBranchResponseData>(
        new CreateBusinessBranchResponseData(id, name, active), message);

public sealed record CreateBusinessBranchResponseData(Guid Id, string Name, bool Active);
