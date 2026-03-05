using IP.IDI.Persistence.Seerders.Entities;

namespace IP.IDI.Persistence.Seerders;

internal sealed class IDISeeder(
    IDIDbContext _context) :
    IApiDBSeeder
{
    private DbSet<AppUserRole> _appUserRoles = default!;
    private DbSet<AppGuide> _guideSet = default!;
    private DbSet<AppRoleClaim> _roleClaims = default!;
    private DbSet<AppRole> _roleSet = default!;
    private DbSet<AppUser> _userSet = default!;

    public void Migrate() =>
        _context.Database.Migrate();

    public void Seed()
    {
        _appUserRoles = _context.Set<AppUserRole>();
        _guideSet = _context.Set<AppGuide>();
        _roleClaims = _context.Set<AppRoleClaim>();
        _roleSet = _context.Set<AppRole>();
        _userSet = _context.Set<AppUser>();

        List<RoleBaseData> roles = [
                SysAdminRole.Create(),
            UserViewerRole.Create(),
            UserEditorRole.Create(),
            UserManagerRole.Create(),
            UserRoleNoClaims.Create(),
            UserListRole.Create(),
            UserCreatorRole.Create()
            ];

        List<UserBaseData> users = [
            ViewerUser.Create(),
            EditorUser.Create(),
            ManagerUser.Create(),
            SysAdminUser.Create(),
            NoClaimsUser.Create(),
            ListUser.Create(),
            CreatorUser.Create()
        ];

        foreach (RoleBaseData roleData in roles)
        {
            AddRoleWithClaims(roleData);
        }

        foreach (UserBaseData user in users)
        {
            AddUserWithRole(user);
        }

        SetInitialPageTutorial();
    }

    private void AddRole(AppRole appRole)
    {
        if (_roleSet.Any(role =>
            role.Name == appRole.Name)) return;

        _roleSet.Add(appRole);
        _context.SaveChanges();
    }

    private void AddRoleClaims(AppRoleClaim roleClaim)
    {
        if (_roleClaims.Any(rc => rc.RoleId == roleClaim.RoleId &&
            rc.ClaimType == JwtCustomClaimNames.Permission &&
            rc.ClaimValue == roleClaim.ClaimValue)) return;

        _roleClaims.Add(roleClaim);
        _context.SaveChanges();
    }

    private void AddRoleWithClaims(RoleBaseData roleData)
    {
        AddRole(roleData.Role);
        foreach (AppRoleClaim roleClaim in roleData.RoleClaims)
            AddRoleClaims(roleClaim);
    }

    private void AddUser(AppUser appUser)
    {
        if (_userSet.Any(user =>
            user.UserName == appUser.UserName)) return;

        _userSet.Add(appUser);
        _context.SaveChanges();
    }

    private void AddUserRole(AppUserRole userRole)
    {
        if (_appUserRoles.Any(ur =>
            ur.UserId == userRole.UserId &&
            ur.RoleId == userRole.RoleId)) return;

        _appUserRoles.Add(userRole);
        _context.SaveChanges();
    }

    private void AddUserWithRole(UserBaseData userData)
    {
        AddUser(userData.User);
        AddUserRole(userData.UserRole);
    }

    private void SetInitialPageTutorial()
    {
        var dashboardGuide = AppGuide.Create("Página Inicial", "pagina-inicial");

        if (_guideSet.Any(x => x.LinkId == dashboardGuide.LinkId)) return;

        _guideSet.Add(dashboardGuide);
        _context.SaveChanges();
    }
}