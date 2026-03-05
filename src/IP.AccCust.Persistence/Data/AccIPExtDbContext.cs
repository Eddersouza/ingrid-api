namespace IP.AccCust.Persistence.Data;

internal class AccIPExtDbContext(
    DbContextOptions<AccIPExtDbContext> options) : DbContext(options)
{
    public static string Schema => DBConstantsIP.Schema;

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
            typeof(AccIPExtDbContext).Assembly);
    }
}

public static class DBConstantsIP
{
    public const string AccountIPMovementsSummaryTable = "AccountsIPMovementsSummary";
    public const string AccountIPTable = "AccountsIP";
    public const string AccountSubscriptionTable = "AccountSubscriptions";
    public const string IntegratorSystemTable = "IntegratorSystems";
    public const string Schema = "AccIPInfo";
}