using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP.Shared.Abstractions.Auths.Claims;

public class AppGuideClaim
{
    public static readonly PermissionClaim All =
           new("AppGuideAll", $"{ResourceType.AppGuide}:{ResourceAction.All}");

    public static readonly PermissionClaim CanCreate =
        new("AppGuideCanCreate", $"{ResourceType.AppGuide}:{ResourceAction.Create}");

    public static readonly PermissionClaim CanDelete =
        new("AppGuideCanDelete", $"{ResourceType.AppGuide}:{ResourceAction.Delete}");

    public static readonly PermissionClaim CanList =
        new("AppGuideCanList", $"{ResourceType.AppGuide}:{ResourceAction.List}");

    public static readonly PermissionClaim CanRead =
        new("AppGuideCanRead", $"{ResourceType.AppGuide}:{ResourceAction.Read}");

    public static readonly PermissionClaim CanUpdate =
        new("AppGuideCanUpdate", $"{ResourceType.AppGuide}:{ResourceAction.Update}");

    public static readonly PermissionClaim CanActivateOrDeactivate =
       new("AppGuideCanActivateOrDeactivate", $"{ResourceType.AppGuide}:{ResourceAction.ActiveOrDeactivate}");

    public static readonly ImmutableDictionary<string, PermissionClaim[]> PermissionList = new[]
    {
        new KeyValuePair<string, PermissionClaim[]>(CanCreate.Name, [CanCreate, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanDelete.Name, [CanDelete, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanList.Name, [CanList, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanRead.Name, [CanRead, All]),
        new KeyValuePair<string, PermissionClaim[]>(CanUpdate.Name, [CanUpdate, All]),
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
