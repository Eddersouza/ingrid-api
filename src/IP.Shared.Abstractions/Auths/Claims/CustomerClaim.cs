namespace IP.Shared.Abstractions.Auths.Claims;

public class CustomerClaim
{
    public static readonly PermissionClaim All =
        new("CustomerAll", $"{ResourceType.Customer}:{ResourceAction.All}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("CustomerCanActivateOrDeactivate", $"{ResourceType.Customer}:{ResourceAction.ActiveOrDeactivate}");

    public static readonly PermissionClaim CanCreate =
            new("CustomerCanCreate", $"{ResourceType.Customer}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("CustomerCanDelete", $"{ResourceType.Customer}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("CustomerCanList", $"{ResourceType.Customer}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("CustomerCanRead", $"{ResourceType.Customer}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("CustomerCanUpdate", $"{ResourceType.Customer}:{ResourceAction.Update}");

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