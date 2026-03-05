namespace IP.Shared.Persistence.Contexts;

internal static class DbContextOptionsBuilderExtension
{
    internal static DbContextOptionsBuilder AddDbConnectionInterceptor(
               this DbContextOptionsBuilder builder,
       string databaseName)
    {
        MySqlConnectionInterceptor dbInterceptor = new(databaseName);

        builder.AddInterceptors(dbInterceptor);

        return builder;
    }

    internal static DbContextOptionsBuilder UseDefaultInterceptors(
        this DbContextOptionsBuilder builder,
        IServiceProvider serviceProvider)
    {
        builder.AddInterceptors(
            serviceProvider
                .GetRequiredService<UpdateDeletableInterceptor>(),
            serviceProvider
                .GetRequiredService<UpdateAuditableInterceptor>(),
            serviceProvider
                .GetRequiredService<CreateAuditTrailInterceptor>(),
            serviceProvider
                .GetRequiredService<DispatcherEventInterceptor>());

        return builder;
    }

    internal static DbContextOptionsBuilder UseDefaultMySqlConnection(
            this DbContextOptionsBuilder builder,
        string connectionString)
    {
        builder.UseMySQL(
            connectionString,
            o => o.UseQuerySplittingBehavior(
                QuerySplittingBehavior.SplitQuery));

        return builder;
    }
}