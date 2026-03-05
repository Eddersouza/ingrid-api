namespace IP.Shared.Abstractions.Auths.Claims;

public class StateClaim
{
    public static readonly PermissionClaim All =
        new("StateAll", $"{ResourceType.State}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("StateCanCreate", $"{ResourceType.State}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("StateCanDelete", $"{ResourceType.State}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("StateCanList", $"{ResourceType.State}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("StateCanRead", $"{ResourceType.State}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("StateCanUpdate", $"{ResourceType.State}:{ResourceAction.Update}");

    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanCreate.Name, [CanCreate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanDelete.Name, [CanDelete, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanList.Name, [CanList, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanRead.Name, [CanRead, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanUpdate.Name, [CanUpdate, All])
    }.ToImmutableDictionary();

    public static readonly string[] PermissionValues =
    [
        All.Claim,
        CanCreate.Claim,
        CanDelete.Claim,
        CanList.Claim,
        CanRead.Claim,
        CanUpdate.Claim
    ];
}
