using IP.Shared.Workers;
using Microsoft.Extensions.Options;
using Quartz;

namespace Ingrid.API.Workers.Seed;

public class SeedWorkerSetup(
    IOptions<SeedWorkerOptions> _options) :
    IConfigureOptions<QuartzOptions>
{
    private readonly SeedWorkerOptions _options = _options.Value;

    public void Configure(QuartzOptions options) =>
        WorkerService.Configure<SeedWorkerService>(options, _options);
}

public static class DependencyInjection
{
    public static IServiceCollection AddSeederWorker(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SeedWorkerOptions>(
           configuration.GetSection(SeedWorkerOptions.NameKey));

        SeedWorkerOptions emailOptions =
            configuration.GetSection(SeedWorkerOptions.NameKey)
            .Get<SeedWorkerOptions>()!;

        if (emailOptions.Enabled)
            services.ConfigureOptions<SeedWorkerSetup>();

        return services;
    }
}