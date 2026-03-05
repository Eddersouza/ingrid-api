using IP.Shared.Abstractions.Extensions;
using IP.Shared.Abstractions.Sessions;
using IP.Shared.Persistence.Data;
using Microsoft.Extensions.Options;
using Quartz;

namespace Ingrid.API.Workers.Seed;

public class SeedWorkerService(
    IServiceProvider _serviceProvider,
    IOptions<SeedWorkerOptions> workerOptions,
    ICurrentUserInfo _currentAppUser,
    ILogger<SeedWorkerService> _logger) : IJob
{
    private SeedWorkerOptions _options = workerOptions.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        if (_options.Enabled) await Task.CompletedTask;

        try
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobName} started",
                nameof(SeedWorkerService),
                _options.JobName);

            _currentAppUser.SetServiceAsUser(_options.JobId, _options.JobName);

            IEnumerable<IApiDBSeeder> services = _serviceProvider.GetServices<IApiDBSeeder>();

            foreach (IApiDBSeeder service in services)
            {
                service.Seed();
            }

            _currentAppUser.RevertAtUser();
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "[{ServiceName}] - {JobName} failed: {StackTrace}",
                nameof(SeedWorkerService),
                _options.JobName,
                exception.GetInnerExceptions());
        }
        finally
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobName} finished",
                nameof(SeedWorkerService),
                _options.JobName);
        }
        await Task.CompletedTask;
    }
}