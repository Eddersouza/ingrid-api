namespace IP.Core.Api.BusinessBranches.UpdateBusinessBranch;

internal sealed record UpdateBusinessBranchCommand(Guid Id, UpdateBusinessBranchRequest Request) :
    ICommand<UpdateBusinessBranchResponse>, ILoggableData;

public class UpdateBusinessBranchRequest
{
    [Required]
    [MinLength(BusinessBranch.NAME_MIN_LENGTH)]
    [MaxLength(BusinessBranch.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;
}
public sealed class UpdateBusinessBranchResponse(
    Guid id,
    string name,
    string message) :
    ResolvedData<UpdateBusinessBranchResponseData>(
        new UpdateBusinessBranchResponseData(id, name), message)
{ };

public sealed record UpdateBusinessBranchResponseData(
    Guid Id, string Name);

