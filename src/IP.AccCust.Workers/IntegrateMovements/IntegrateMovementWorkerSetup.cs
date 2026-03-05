namespace IP.AccCust.Workers.IntegrateMovements;

internal sealed class IntegrateMovementWorkerSetup(
    IOptions<IntegrateMovementWorkerOptions> _options) :
    IConfigureOptions<QuartzOptions>
{
    private readonly IntegrateMovementWorkerOptions _options = _options.Value;

    public void Configure(QuartzOptions options) =>
        WorkerService.Configure<IntegrateMovementWorkerService>(options, _options);
}