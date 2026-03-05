namespace IP.Shared.Abstractions.Auths.Claims;

public class AccountSubscriptionClaim
{
    public static readonly PermissionClaim All =
        new("AccountSubscriptionAll", $"{ResourceType.AccountSubscription}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("AccountSubscriptionCanCreate", $"{ResourceType.AccountSubscription}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("AccountSubscriptionCanDelete", $"{ResourceType.AccountSubscription}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("AccountSubscriptionCanList", $"{ResourceType.AccountSubscription}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("AccountSubscriptionCanRead", $"{ResourceType.AccountSubscription}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("AccountSubscriptionCanUpdate", $"{ResourceType.AccountSubscription}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("AccountSubscriptionCanActivateOrDeactivate", $"{ResourceType.AccountSubscription}:{ResourceAction.ActiveOrDeactivate}");

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
