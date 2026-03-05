namespace IP.Shared.Abstractions.Auths.Claims;

public class BusinessBranchClaim
{
    public static readonly PermissionClaim All =
        new("BusinessBranchAll", $"{ResourceType.BusinessBranch}:{ResourceAction.All}");
    public static readonly PermissionClaim CanCreate =
        new("BusinessBranchCanCreate", $"{ResourceType.BusinessBranch}:{ResourceAction.Create}");
    public static readonly PermissionClaim CanDelete =
        new("BusinessBranchCanDelete", $"{ResourceType.BusinessBranch}:{ResourceAction.Delete}");
    public static readonly PermissionClaim CanList =
        new("BusinessBranchCanList", $"{ResourceType.BusinessBranch}:{ResourceAction.List}");
    public static readonly PermissionClaim CanRead =
        new("BusinessBranchCanRead", $"{ResourceType.BusinessBranch}:{ResourceAction.Read}");
    public static readonly PermissionClaim CanUpdate =
        new("BusinessBranchCanUpdate", $"{ResourceType.BusinessBranch}:{ResourceAction.Update}");
    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("BusinessBranchCanActivateOrDeactivate", $"{ResourceType.BusinessBranch}:{ResourceAction.ActiveOrDeactivate}");
    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanCreate.Name, [CanCreate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanDelete.Name, [CanDelete, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanList.Name, [CanList, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanRead.Name, [CanRead, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanUpdate.Name, [CanUpdate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanActivateOrDeactivate.Name, [CanActivateOrDeactivate, All]),
    }.ToImmutableDictionary();
    public static readonly string[] PermissionValues =
    [
        All.Claim,
        CanCreate.Claim,
        CanDelete.Claim,
        CanList.Claim,
        CanRead.Claim,
        CanUpdate.Claim,
        CanActivateOrDeactivate.Claim
    ];
}
