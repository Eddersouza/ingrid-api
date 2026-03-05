namespace IP.Core.Persistence.Data;

internal class CoreDbContext(
    DbContextOptions<CoreDbContext> options) : DbContext(options)
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
            typeof(CoreDbContext).Assembly);
    }
}

public static class DBConstants
{
    public const string AccountSubscriptionTable = "AccountSubscriptions";
    public const string AddressTable = "Addresses";
    public const string BusinessBranchTable = "BusinessBranches";
    public const string BusinessSegmentTable = "BusinessSegments";
    public const string CityTable = "Cities";
    public const string CustomerTable = "Customers";
    public const string EmployeeTable = "Employees";
    public const string IntegratorSystemTable = "IntegratorSystems";
    public const string Schema = "Core";
    public const string StateTable = "States";
}