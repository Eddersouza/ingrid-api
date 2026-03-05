namespace IP.Shared.Abstractions.Auths.Claims;

public class AccountIPClaim
{
    public static readonly PermissionClaim All =
        new("AccountIPAll", $"{ResourceType.AccountIP}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("AccountIPCanCreate", $"{ResourceType.AccountIP}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanList =
        new("AccountIPCanList", $"{ResourceType.AccountIP}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("AccountIPCanRead", $"{ResourceType.AccountIP}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("AccountIPCanUpdate", $"{ResourceType.AccountIP}:{ResourceAction.Update}");

    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanCreate.Name, [CanCreate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanList.Name, [CanList, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanRead.Name, [CanRead, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanUpdate.Name, [CanUpdate, All]),
    }.ToImmutableDictionary();

    public static readonly string[] PermissionValues =
    [
        All.Claim,
        CanCreate.Claim,
        CanList.Claim,
        CanRead.Claim,
        CanUpdate.Claim,
    ];
}