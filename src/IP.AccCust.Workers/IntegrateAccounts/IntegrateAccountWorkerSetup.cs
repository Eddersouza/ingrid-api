namespace IP.AccCust.Workers.IntegrateAccounts;

internal sealed class IntegrateAccountWorkerSetup(
    IOptions<IntegrateAccountWorkerOptions> _options) :
    IConfigureOptions<QuartzOptions>
{
    private readonly IntegrateAccountWorkerOptions _options = _options.Value;

    public void Configure(QuartzOptions options) =>
        WorkerService.Configure<IntegrateAccountWorkerService>(options, _options);
}