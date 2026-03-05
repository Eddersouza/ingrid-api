namespace IP.AccIPInfo.Persistence.Data;

internal class AccIPDbContext(
    DbContextOptions<AccIPDbContext> options) : DbContext(options)
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
            typeof(AccIPDbContext).Assembly);
    }
}

public static class DBConstants
{
    public const string AccountIPMovementsSummaryTable = "AccountsIPMovementsSummary";
    public const string AccountIPTable = "AccountsIP";
    public const string AccountSubscriptionTable = "AccountSubscriptions";
    public const string IntegratorSystemTable = "IntegratorSystems";
    public const string Schema = "AccIPInfo";
}