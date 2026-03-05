namespace IP.Com.Workers.EmailsSchedule;

[DisallowConcurrentExecution]
internal sealed class SendEmailWorkerService(
    IComUoW _unitOfWork,
    IOptions<SendEmailWorkerOptions> options,
    ICurrentUserInfo _currentAppUser,
    IEmailService _emailService,
    ILogger<SendEmailWorkerService> _logger) : IJob
{
    private readonly SendEmailWorkerOptions _options = options.Value;
    private readonly IEmailScheduleRepository _emailScheduleRepository =
          _unitOfWork.GetRepository<IEmailScheduleRepository>();

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_options.Enabled) return;
        try
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobName} started",
                nameof(SendEmailWorkerService),
                _options.JobName);

            _currentAppUser.SetServiceAsUser(_options.JobId, _options.JobName);
            await SendEmails();
            _currentAppUser.RevertAtUser();
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "[{ServiceName}] - {JobName} failed: {StackTrace}",
                nameof(SendEmailWorkerService),
                _options.JobName,
                exception.GetInnerExceptions());
        }
        finally
        {
            _logger.LogInformation(
                "[{ServiceName}] - {JobName} finished",
                nameof(SendEmailWorkerService),
                _options.JobName);
        }
    }

    private async Task SendEmails()
    {
        EmailSchedule? emailSchedule =
            await _emailScheduleRepository
                .Entities
                .Where(email => !email.Sended &&
                    (email.LastAttemptDate < DateTime.Now.AddMinutes(1) || 
                        email.LastAttemptDate == null) &&
                    email.Attempts < EmailSchedule.MAX_ATTEMPTS)
                .OrderBy(email => email.LastAttemptDate)
                .FirstOrDefaultAsync();

        if (emailSchedule is null) return;

        try
        {
            CancellationTokenSource cancellationTokenSource =
                new(TimeSpan.FromMinutes(2));


            emailSchedule.NewAttempt();

            _logger.LogInformation(
                "[{ServiceName}] - {JobName} send email attempt {Attempt}: {From} to {To}",
                nameof(SendEmailWorkerService),
                _options.JobName,
                emailSchedule.Attempts,
                emailSchedule.Sender,
                emailSchedule.Recipient);

            await _emailService.SendEmail(
                emailSchedule.Sender,
                [.. emailSchedule.Recipient.Split(';')],
                [],
                [],
                emailSchedule.Subject,
                emailSchedule.Body,
                cancellationTokenSource.Token);

            emailSchedule.SetSended();

            _emailScheduleRepository.Update(emailSchedule);
            await _unitOfWork.SaveChangesAsync(cancellationTokenSource.Token);
        }
        catch (Exception exception)
        {
            emailSchedule.SetErrorMessages(exception.GetInnerExceptions());
            _emailScheduleRepository.Update(emailSchedule);
            await _unitOfWork.SaveChangesAsync(CancellationToken.None);
            throw;
        }


    }
}