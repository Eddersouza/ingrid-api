namespace IP.Shared.Abstractions.Auths;

public static class ClaimsInfo
{
    public static ImmutableDictionary<string, PermissionClaim[]> GetPermissionList() =>
        UserClaim.PermissionList
        .AddRange(RoleClaim.PermissionList)
        .AddRange(StateClaim.PermissionList)
        .AddRange(CityClaim.PermissionList)
        .AddRange(AccountSubscriptionClaim.PermissionList)
        .AddRange(IntegratorSystemClaim.PermissionList)
        .AddRange(AppGuideClaim.PermissionList)
        .AddRange(BusinessBranchClaim.PermissionList)
        .AddRange(BusinessSegmentClaim.PermissionList)
        .AddRange(AddressClaim.PermissionList)
        .AddRange(EmployeeClaim.PermissionList)
        .AddRange(CustomerClaim.PermissionList)
        .AddRange(AccountIPClaim.PermissionList)
        .AddRange(TransactionIPTotalByOwnerClaim.PermissionList);

    public static IEnumerable<string> GetPermissionValueList() =>
        UserClaim.PermissionValues
        .Concat(RoleClaim.PermissionValues)
        .Concat(StateClaim.PermissionValues)
        .Concat(CityClaim.PermissionValues)
        .Concat(AccountSubscriptionClaim.PermissionValues)
        .Concat(IntegratorSystemClaim.PermissionValues)
        .Concat(AppGuideClaim.PermissionValues)
        .Concat(BusinessBranchClaim.PermissionValues)
        .Concat(BusinessSegmentClaim.PermissionValues)
        .Concat(AddressClaim.PermissionValues)
        .Concat(EmployeeClaim.PermissionValues)
        .Concat(CustomerClaim.PermissionValues)
        .Concat(AccountIPClaim.PermissionValues)
        .Concat(TransactionIPTotalByOwnerClaim.PermissionValues);
}