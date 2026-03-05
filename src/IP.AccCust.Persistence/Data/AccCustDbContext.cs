namespace IP.AccCust.Persistence.Data;

internal sealed class AccCustDbContext(DbContextOptions<AccCustDbContext> options) :
    DbContext(options)
{
    public static string Schema => DBConstants.Schema;

    protected override void ConfigureConventions(
          ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            typeof(AccCustDbContext).Assembly);
    }
}

public static class DBConstants
{
    public const string AccountMovementView = "vw_sub_movement";
    public const string AccountView = "vw_sub_account";
    public const string PlanView = "vw_sub_plan";
    public const string Schema = "bancodigital_bi";
}