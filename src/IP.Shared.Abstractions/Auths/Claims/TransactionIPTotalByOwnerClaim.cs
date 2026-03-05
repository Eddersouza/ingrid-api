namespace IP.Shared.Abstractions.Auths.Claims;

public class TransactionIPTotalByOwnerClaim
{
    public static readonly PermissionClaim All =
        new("TransactionIPAll", $"{ResourceType.TransactionIPTotalByOwner}:{ResourceAction.All}");

    public static readonly PermissionClaim CanViewTeamData =
        new("TransactionIPCanViewTeamData", $"{ResourceType.TransactionIPTotalByOwner}:{ResourceAction.ViewTeamData}");

    public static readonly PermissionClaim CanViewInternalData =
        new("TransactionIPCanViewInternalData", $"{ResourceType.TransactionIPTotalByOwner}:{ResourceAction.ViewInternalData}");

    public static readonly PermissionClaim CanViewOwnData =
       new("TransactionIPCanVieOwnData", $"{ResourceType.TransactionIPTotalByOwner}:{ResourceAction.ViewOwnData}");


    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanViewTeamData.Name, [CanViewTeamData, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanViewInternalData.Name, [CanViewInternalData, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanViewOwnData.Name, [CanViewOwnData, All]),

    }.ToImmutableDictionary();

    public static readonly string[] PermissionValues =
    [
        All.Claim,
        CanViewTeamData.Claim,
        CanViewInternalData.Claim
    ];
}
