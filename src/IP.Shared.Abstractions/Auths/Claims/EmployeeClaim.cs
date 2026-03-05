namespace IP.Shared.Abstractions.Auths.Claims;

public class EmployeeClaim
{
    public static readonly PermissionClaim All =
        new("EmployeeAll", $"{ResourceType.Employee}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("EmployeeCanCreate", $"{ResourceType.Employee}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("EmployeeCanDelete", $"{ResourceType.Employee}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("EmployeeCanList", $"{ResourceType.Employee}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("EmployeeCanRead", $"{ResourceType.Employee}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("EmployeeCanUpdate", $"{ResourceType.Employee}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("EmployeeCanActivateOrDeactivate", $"{ResourceType.Employee}:{ResourceAction.ActiveOrDeactivate}");

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
