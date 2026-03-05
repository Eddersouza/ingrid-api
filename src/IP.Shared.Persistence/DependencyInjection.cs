namespace IP.Shared.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddDBInterceptors(
           this IServiceCollection services)
    {
        services.AddScoped<UpdateDeletableInterceptor>();
        services.AddScoped<UpdateAuditableInterceptor>();
        services.AddScoped<CreateAuditTrailInterceptor>();
        services.AddScoped<DispatcherEventInterceptor>();

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        services.RegisterUnitOfWork(assembly);
        services.RegisterDbContext(configuration, assembly);

        return services;
    }

    public static void ExecuteMigrations(
        this IServiceCollection services,
        Assembly[] assemblies)
    {
        services.Scan(scan => scan
           .FromAssemblies(assemblies)
           .AddClasses(classes => classes
               .AssignableTo<IApiDBSeeder>(),
                   publicOnly: false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());

        var serviceProvider = services.BuildServiceProvider();
        IEnumerable<IApiDBSeeder> myServices =
            serviceProvider.GetServices<IApiDBSeeder>();

        foreach (var service in myServices)
        {
            service.Migrate();
        }
    }

    private static IServiceCollection AddDbContext<TDbContext>(
        this IServiceCollection services,
        string connectionString,
        ApiDbConnectionOptionsEnum provider) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(
               (serviceProvider, options) =>
               {
                   if (provider == ApiDbConnectionOptionsEnum.Mysql)
                       options.UseDefaultMySqlConnection(connectionString)
                       .UseDefaultInterceptors(serviceProvider);

                   if (provider == ApiDbConnectionOptionsEnum.Postgres)
                       options.UseNpgsql(connectionString)
                       .UseSnakeCaseNamingConvention()
                       .UseDefaultInterceptors(serviceProvider);
               });

        return services;
    }

    private static IServiceCollection RegisterDbContext(
                this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        services.Configure<ApiDbOptions>(
           configuration.GetSection(ApiDbOptions.NameKey));

        ApiDbOptions? dbOptions =
            configuration.GetSection(ApiDbOptions.NameKey)
            .Get<ApiDbOptions>();

        var dbContextTypes = assembly
          .GetTypes()
          .Where(t =>
              t.IsClass &&
              !t.IsAbstract &&
              typeof(DbContext).IsAssignableFrom(t));

        foreach (var dbContextType in dbContextTypes)
        {
            var schemaProperty = dbContextType.GetProperty(
                "Schema",
                BindingFlags.Public | BindingFlags.Static);

            var schemaName = schemaProperty?.GetValue(null)?.ToString();

            string connectionString = dbOptions!.Connections.FirstOrDefault(c => c.Id == schemaName)!.Connection;
            ApiDbConnectionOptionsEnum provider = dbOptions!.Connections.FirstOrDefault(c => c.Id == schemaName)!.Provider;


            var method = typeof(DependencyInjection)
                .GetMethod(
                    nameof(AddDbContext),
                    BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Public)
                ?.MakeGenericMethod(dbContextType);

            method?.Invoke(null, [services, connectionString, provider]);
        }

        return services;
    }

    private static IServiceCollection RegisterUnitOfWork(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.Scan(scan => scan
           .FromAssemblies(assembly)
           .AddClasses(classes => classes
               .AssignableTo<IUnitOfWork>()
               .Where(type => type.Name != "UnitOfWork"),
                   publicOnly: false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());

        return services;
    }
}