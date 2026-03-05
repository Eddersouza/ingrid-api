namespace IP.Com.Workers;

public static class DependencyInjection
{
    public static IServiceCollection AddComWorkers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SendEmailWorkerOptions>(
           configuration.GetSection(SendEmailWorkerOptions.NameKey));

        SendEmailWorkerOptions emailOptions =
            configuration.GetSection(SendEmailWorkerOptions.NameKey)
            .Get<SendEmailWorkerOptions>()!;

        if (emailOptions.Enabled)
            services.ConfigureOptions<SendEmailWorkerSetup>();

        return services;
    }
}
