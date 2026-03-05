namespace IP.Shared.Emails.Services;

internal interface ISMTPServiceEmail : IEmailServiceBase;

internal class SMTPServiceEmail(
    IOptions<SMTPServiceEmailOptions> option) : ISMTPServiceEmail
{
    private readonly SMTPServiceEmailOptions _options = option.Value;

    public async Task SendEmail(
       string from,
       List<string> to,
       List<string> ccAddresses,
       List<string> bccAddresses,
       string subject,
       string body,
       CancellationToken cancellationToken)
    {
        SmtpSender sender = new(() =>
            new SmtpClient(_options.Host, _options.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                Environment.GetEnvironmentVariable(_options.Username),
                Environment.GetEnvironmentVariable(_options.Password)),
                EnableSsl = _options.EnableSsl,
            });

        Email.DefaultSender = sender;

        IFluentEmail email = Email
            .From(from)
            .Subject(subject)
            .Body(body, true);

        foreach (var recipient in to.Distinct())
            email = email.To(recipient);

        foreach (var cc in ccAddresses.Distinct())
            email = email.CC(cc);

        foreach (var bcc in bccAddresses.Distinct())
            email = email.BCC(bcc);

        SendResponse result = await email.SendAsync();

        if (!result.Successful)
            throw new Exception("Failed to send email. Errors: " + string.Join(", ", result.ErrorMessages));
    }
}