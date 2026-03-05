namespace IP.Com.Workers.EmailsSchedule;

internal sealed class SendEmailWorkerSetup(
    IOptions<SendEmailWorkerOptions> _options) :
    IConfigureOptions<QuartzOptions>
{
    private readonly SendEmailWorkerOptions _options = _options.Value;

    public void Configure(QuartzOptions options) =>
        WorkerService.Configure<SendEmailWorkerService>(options, _options);
}