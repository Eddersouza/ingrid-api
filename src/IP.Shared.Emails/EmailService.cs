namespace IP.Shared.Emails;

public interface IEmailServiceBase
{
    Task SendEmail(
        string from,
        List<string> to,
        List<string> ccAddresses,
        List<string> bccAddresses,
        string subject,
        string body,
        CancellationToken cancellationToken);
}

public interface IEmailService: IEmailServiceBase;

internal sealed class EmailService(
    ISMTPServiceEmail smtpService,    
    IOptions<SMTPServiceEmailOptions> option) : IEmailService
{
    //private readonly AWSServiceEmailOptions _awsOptions = optionAWS.Value;
    private readonly SMTPServiceEmailOptions _smtpOptions = option.Value;
    //private readonly IAWSServiceEmail _awsService = awsService;
    private readonly ISMTPServiceEmail _smtpService = smtpService;

    public async Task SendEmail(
        string from, 
        List<string> to, 
        List<string> ccAddresses, 
        List<string> bccAddresses, 
        string subject, 
        string body,
        CancellationToken cancellationToken)
    {
        //if(_awsOptions.Enabled)
            //await _awsService.SendEmailAsync(
            //    from, 
            //    to, 
            //    ccAddresses, 
            //    bccAddresses, 
            //    subject, 
            //    body, 
            //    cancellationToken);
        if(_smtpOptions.Enabled)
            await _smtpService.SendEmail(
                from, 
                to, 
                ccAddresses, 
                bccAddresses, 
                subject, 
                body, 
                cancellationToken);

    }
}