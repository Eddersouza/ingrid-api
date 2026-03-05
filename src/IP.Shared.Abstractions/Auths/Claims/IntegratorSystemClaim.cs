namespace IP.Shared.Abstractions.Auths.Claims;

public class IntegratorSystemClaim
{
    public static readonly PermissionClaim All =
        new("IntegratorSystemAll", $"{ResourceType.IntegratorSystem}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("IntegratorSystemCanCreate", $"{ResourceType.IntegratorSystem}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("IntegratorSystemCanDelete", $"{ResourceType.IntegratorSystem}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("IntegratorSystemCanList", $"{ResourceType.IntegratorSystem}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("IntegratorSystemCanRead", $"{ResourceType.IntegratorSystem}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("IntegratorSystemCanUpdate", $"{ResourceType.IntegratorSystem}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("IntegratorSystemCanActivateOrDeactivate", $"{ResourceType.IntegratorSystem}:{ResourceAction.ActiveOrDeactivate}");

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
