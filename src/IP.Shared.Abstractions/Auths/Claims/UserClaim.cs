namespace IP.Shared.Abstractions.Auths.Claims;

public class UserClaim
{
    public static readonly PermissionClaim All =
        new("UserAll", $"{ResourceType.User}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("UserCanCreate", $"{ResourceType.User}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanChangePassword =
        new("UserCanChangePassword", $"{ResourceType.User}:{ResourceAction.ChangePassword}");

    public static readonly PermissionClaim CanDelete =
        new("UserCanDelete", $"{ResourceType.User}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("UserCanList", $"{ResourceType.User}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("UserCanRead", $"{ResourceType.User}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("UserCanUpdate", $"{ResourceType.User}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
        new("UserCanActivateOrDeactivate", $"{ResourceType.User}:{ResourceAction.ActiveOrDeactivate}");

    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanCreate.Name, [CanCreate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanDelete.Name, [CanDelete, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanList.Name, [CanList, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanRead.Name, [CanRead, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanUpdate.Name, [CanUpdate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanChangePassword.Name, [CanChangePassword, All]),
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
