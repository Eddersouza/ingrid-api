namespace IP.Shared.Abstractions.Auths.Claims;

public class BusinessSegmentClaim
{
    public static readonly PermissionClaim All =
        new("BusinessSegmentAll", $"{ResourceType.BusinessSegment}:{ResourceAction.All}");
    public static readonly PermissionClaim CanCreate =
        new("BusinessSegmentCanCreate", $"{ResourceType.BusinessSegment}:{ResourceAction.Create}");
    public static readonly PermissionClaim CanDelete =
        new("BusinessSegmentCanDelete", $"{ResourceType.BusinessSegment}:{ResourceAction.Delete}");
    public static readonly PermissionClaim CanList =
        new("BusinessSegmentCanList", $"{ResourceType.BusinessSegment}:{ResourceAction.List}");
    public static readonly PermissionClaim CanRead =
        new("BusinessSegmentCanRead", $"{ResourceType.BusinessSegment}:{ResourceAction.Read}");
    public static readonly PermissionClaim CanUpdate =
        new("BusinessSegmentCanUpdate", $"{ResourceType.BusinessSegment}:{ResourceAction.Update}");
    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("BusinessSegmentCanActivateOrDeactivate", $"{ResourceType.BusinessSegment}:{ResourceAction.ActiveOrDeactivate}");
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
