namespace IP.IDI.Persistence.Data;

internal sealed class IDIDbContext(
    DbContextOptions<IDIDbContext> options) :
    IdentityDbContext<AppUser,
        AppRole,
        Guid,
        AppUserClaim,
        AppUserRole,
        AppUserLogin,
        AppRoleClaim,
        AppUserToken>(options)
{
    public static string Schema => DBConstants.Schema;

    protected override void ConfigureConventions(
           ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddDefaultStringConfiguration();

        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuditTrailEFMap());
        builder.ApplyConfigurationsFromAssembly(
            typeof(IDIDbContext).Assembly);
    }
}

public static class DBConstants
{
    public const string AppGuidesTable = "AppGuides";
    public const string AppGuideViewedTable = "AppGuideViewed";
    public const string AppRoleClaimsTable = "RoleClaims";
    public const string AppRolesTable = "Roles";
    public const string AppUserClaimsTable = "UserClaims";
    public const string AppUserLoginsTable = "UserLogins";
    public const string AppUserRolesTable = "UserRoles";
    public const string AppUsersTable = "Users";
    public const string AppUserTokensTable = "UserTokens";
    public const string ClientTable = "Clients";
    public const string Schema = "IDI";
}