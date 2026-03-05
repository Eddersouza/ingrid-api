namespace IP.Com.Persistence.Data;

internal class ComDbContext(DbContextOptions<ComDbContext> options) :
    DbContext(options)
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
            typeof(ComDbContext).Assembly);
    }
}

public static class DBConstants
{
    public const string EmailSchedule = "EmailsSchedule";
    public const string Schema = "Com";
}