namespace IP.Shared.Abstractions.Auths.Claims;

public class RoleClaim
{
    public static readonly PermissionClaim All =
        new("RoleAll", $"{ResourceType.Role}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("RoleCanCreate", $"{ResourceType.Role}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("RoleCanDelete", $"{ResourceType.Role}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("RoleCanList", $"{ResourceType.Role}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("RoleCanRead", $"{ResourceType.Role}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("RoleCanUpdate", $"{ResourceType.Role}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("RoleCanActivateOrDeactivate", $"{ResourceType.Role}:{ResourceAction.ActiveOrDeactivate}");

    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanCreate.Name, [CanCreate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanDelete.Name, [CanDelete, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanList.Name, [CanList, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanRead.Name, [CanRead, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanUpdate.Name, [CanUpdate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanActivateOrDeactivate.Name, [CanActivateOrDeactivate, All])
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