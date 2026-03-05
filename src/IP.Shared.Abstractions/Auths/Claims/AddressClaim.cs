namespace IP.Shared.Abstractions.Auths.Claims;

public class AddressClaim
{
    public static readonly PermissionClaim All =
        new("AddressAll", $"{ResourceType.Address}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("AddressCanCreate", $"{ResourceType.Address}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("AddressCanDelete", $"{ResourceType.Address}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("AddressCanList", $"{ResourceType.Address}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("AddressCanRead", $"{ResourceType.Address}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("AddressCanUpdate", $"{ResourceType.Address}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("AddressCanActivateOrDeactivate", $"{ResourceType.Address}:{ResourceAction.ActiveOrDeactivate}");

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
