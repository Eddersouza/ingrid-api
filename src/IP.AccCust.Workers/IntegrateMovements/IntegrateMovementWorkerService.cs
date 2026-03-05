namespace IP.AccCust.Workers.IntegrateMovements;

[DisallowConcurrentExecution]
internal sealed class IntegrateMovementWorkerService(
    ICurrentUserInfo _currentAppUser,
    IOptions<IntegrateMovementWorkerOptions> options,
    ICommandHandler<IntegrateMovementsCommand, IntegrateMovementsResponse> commandHandler,
    ILogger<IntegrateMovementWorkerService> _logger) : IJob
{
    private readonly IntegrateMovementWorkerOptions _options = options.Value;

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_options.Enabled) return;
        try
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobGroup}:{JobName} started",
                nameof(IntegrateMovementWorkerService),
                _options.JobGroup,
                _options.JobName);
            _currentAppUser.SetServiceAsUser(_options.JobId, _options.JobName);
            await commandHandler.Handle(new IntegrateMovementsCommand(), default);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "[{ServiceName}] - {JobGroup}:{JobName} failed: {StackTrace}",
                nameof(IntegrateMovementWorkerService),
                _options.JobGroup,
                _options.JobName,
                exception.GetInnerExceptions());
        }
        finally
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobGroup}:{JobName} finished",
                nameof(IntegrateMovementWorkerService),
                _options.JobGroup,
                _options.JobName);

            _currentAppUser.RevertAtUser();
        }
    }
}