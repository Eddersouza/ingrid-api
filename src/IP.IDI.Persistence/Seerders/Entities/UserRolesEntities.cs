namespace IP.IDI.Persistence.Seerders.Entities;

internal sealed class SysAdminRole : RoleBaseData
{
    public static readonly Guid SYSADMIN_ROLE_ID = Guid.Parse("0198d3e8-4b07-778a-8800-39c7924959ad");

    public SysAdminRole()
    {
        Role = new AppRole { Id = SYSADMIN_ROLE_ID, Name = DefaultRole.SysAdmin };
        Role.ActivableInfo.SetAsActive();
        RoleClaims.Add(CreateRoleClaim(UserClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(AppGuideClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.All.Claim));
        RoleClaims.Add(CreateRoleClaim(TransactionIPTotalByOwnerClaim.All.Claim));
    }

    public static SysAdminRole Create() => new();
}

internal sealed class UserEditorRole : RoleBaseData
{
    public static readonly Guid USER_VIEWER_ROLE_ID = Guid.Parse("01995a3c-492b-7cca-90c3-3f41fb7f5d1f");

    public UserEditorRole()
    {
        Role = new AppRole { Id = USER_VIEWER_ROLE_ID, Name = "UserEditor" };
        Role.ActivableInfo.SetAsActive();
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanUpdate.Claim));
        RoleClaims.Add(CreateRoleClaim(TransactionIPTotalByOwnerClaim.CanViewOwnData.Claim));
    }

    public static UserEditorRole Create() => new();
}

internal sealed class UserListRole : RoleBaseData
{
    public static readonly Guid USER_VIEWER_ROLE_ID = Guid.Parse("01997e41-817d-7269-b58e-cd0d934e38e7");

    public UserListRole()
    {
        Role = new AppRole { Id = USER_VIEWER_ROLE_ID, Name = "UserList" };
        Role.ActivableInfo.SetAsActive();
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(TransactionIPTotalByOwnerClaim.CanViewInternalData.Claim));
    }

    public static UserListRole Create() => new();
}

internal sealed class UserManagerRole : RoleBaseData
{
    public static readonly Guid USER_VIEWER_ROLE_ID = Guid.Parse("01995a3c-492b-7c2f-8fde-37975ff835ac");

    public UserManagerRole()
    {
        Role = new AppRole { Id = USER_VIEWER_ROLE_ID, Name = "UserManager" };
        Role.ActivableInfo.SetAsActive();
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanActivateOrDeactivate.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanDelete.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(TransactionIPTotalByOwnerClaim.CanViewTeamData.Claim));
    }

    public static UserManagerRole Create() => new();
}

internal sealed class UserRoleNoClaims : RoleBaseData
{
    public static readonly Guid USER_VIEWER_ROLE_ID = Guid.Parse("01995a73-dfd7-7907-98a3-aaa3c63abe57");

    public UserRoleNoClaims()
    {
        Role = new AppRole { Id = USER_VIEWER_ROLE_ID, Name = "UserRoleNoClaims" };
        Role.ActivableInfo.SetAsActive();
    }

    public static UserRoleNoClaims Create() => new();
}

internal sealed class UserViewerRole : RoleBaseData
{
    public static readonly Guid USER_VIEWER_ROLE_ID = Guid.Parse("01995a3c-492b-7259-b883-5da8a0d9ac8f");

    public UserViewerRole()
    {
        Role = new AppRole { Id = USER_VIEWER_ROLE_ID, Name = "UserViewer" };
        Role.ActivableInfo.SetAsActive();
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanRead.Claim));
    }

    public static UserViewerRole Create() => new();
}

internal sealed class UserCreatorRole : RoleBaseData
{
    public static readonly Guid USER_VIEWER_ROLE_ID = Guid.Parse("01997e41-817d-702b-b75b-485d4f57fb37");

    public UserCreatorRole()
    {
        Role = new AppRole { Id = USER_VIEWER_ROLE_ID, Name = "UserCreator" };
        Role.ActivableInfo.SetAsActive();
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(UserClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(RoleClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(StateClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CityClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountSubscriptionClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(IntegratorSystemClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessBranchClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(BusinessSegmentClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AddressClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(EmployeeClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(CustomerClaim.CanCreate.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanList.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanRead.Claim));
        RoleClaims.Add(CreateRoleClaim(AccountIPClaim.CanCreate.Claim));
    }

    public static UserCreatorRole Create() => new();
}