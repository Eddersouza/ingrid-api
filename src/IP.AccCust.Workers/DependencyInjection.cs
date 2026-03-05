namespace IP.AccCust.Workers;

public static class DependencyInjection
{
    public static IServiceCollection AddAccCustWorkers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ConfigureAccountIntegration(services, configuration);
        ConfigureMovementIntegration(services, configuration);

        return services;
    }

    private static void ConfigureAccountIntegration(
        IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<IntegrateAccountWorkerOptions>(
                   configuration.GetSection(IntegrateAccountWorkerOptions.NameKey));

        IntegrateAccountWorkerOptions integrateAccountWorkerOptions =
            configuration.GetSection(IntegrateAccountWorkerOptions.NameKey)
            .Get<IntegrateAccountWorkerOptions>()!;

        if (integrateAccountWorkerOptions.Enabled)
            services.ConfigureOptions<IntegrateAccountWorkerSetup>();
    }

    private static void ConfigureMovementIntegration(
        IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<IntegrateMovementWorkerOptions>(
                   configuration.GetSection(IntegrateMovementWorkerOptions.NameKey));

        IntegrateMovementWorkerOptions integrateMovementWorkerOptions =
            configuration.GetSection(IntegrateMovementWorkerOptions.NameKey)
            .Get<IntegrateMovementWorkerOptions>()!;

        if (integrateMovementWorkerOptions.Enabled)
            services.ConfigureOptions<IntegrateMovementWorkerSetup>();
    }
}