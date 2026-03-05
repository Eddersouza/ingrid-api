namespace IP.Shared.Abstractions.Auths.Claims;

public class CityClaim
{
    public static readonly PermissionClaim All =
        new("CityAll", $"{ResourceType.City}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("CityCanCreate", $"{ResourceType.City}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("CityCanDelete", $"{ResourceType.City}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("CityCanList", $"{ResourceType.City}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("CityCanRead", $"{ResourceType.City}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("CityCanUpdate", $"{ResourceType.City}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("CityCanActivateOrDeactivate", $"{ResourceType.City}:{ResourceAction.ActiveOrDeactivate}");

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
