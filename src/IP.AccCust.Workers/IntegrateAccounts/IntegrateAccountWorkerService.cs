namespace IP.AccCust.Workers.IntegrateAccounts;

[DisallowConcurrentExecution]
internal sealed class IntegrateAccountWorkerService(
    ICurrentUserInfo _currentAppUser,
    ICommandHandler<IntegrateAccountCommand, IntegrateAccountResponse> commandHandler,
    IOptions<IntegrateAccountWorkerOptions> options,
    ILogger<IntegrateAccountWorkerService> _logger) : IJob
{
    private readonly IntegrateAccountWorkerOptions _options = options.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_options.Enabled) return;
        try
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobGroup}:{JobName} started",
                nameof(IntegrateAccountWorkerService),
                _options.JobGroup,
                _options.JobName);
            _currentAppUser.SetServiceAsUser(_options.JobId, _options.JobName);
            await commandHandler.Handle(new IntegrateAccountCommand(), default);
            _currentAppUser.RevertAtUser();
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "[{ServiceName}] - {JobGroup}:{JobName} failed: {StackTrace}",
                nameof(IntegrateAccountWorkerService),
                _options.JobGroup,
                _options.JobName,
                exception.GetInnerExceptions());
        }
        finally
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobGroup}:{JobName} finished",
                nameof(IntegrateAccountWorkerService),
                _options.JobGroup,
                _options.JobName);
        }
    }
}