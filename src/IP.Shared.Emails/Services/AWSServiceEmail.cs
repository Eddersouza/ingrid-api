namespace IP.Shared.Emails.Services;

internal interface IAWSServiceEmail
{
    Task SendEmailAsync(string from,
       List<string> to,
       List<string> ccAddresses,
       List<string> bccAddresses,
       string subject,
       string body,
       CancellationToken cancellationToken);
}

internal sealed class AWSServiceEmail : IAWSServiceEmail
{
    private readonly AWSServiceEmailOptions _options;
    private readonly IAmazonSimpleEmailServiceV2 _sesClient;

    public AWSServiceEmail(
        IOptions<AWSServiceEmailOptions> option)
    {        
        _options = option.Value;

        if (_options.ServiceUrl.IsNotNullOrWhiteSpace())
            _sesClient = LocalstackService;
        else _sesClient = AwsService;
    }

    private IAmazonSimpleEmailServiceV2 AwsService =>
        new AmazonSimpleEmailServiceV2Client(
            new BasicAWSCredentials(_options.AccessKey, _options.SecretKey),
            RegionEndpoint.GetBySystemName(_options.Region)
            );

    private IAmazonSimpleEmailServiceV2 LocalstackService =>
        new AmazonSimpleEmailServiceV2Client(
            new BasicAWSCredentials(_options.AccessKey, _options.SecretKey),
            new AmazonSimpleEmailServiceV2Config
            {
                ServiceURL = _options.ServiceUrl,
                AuthenticationRegion = _options.Region
            }
        );

    public async Task SendEmailAsync(string from,
       List<string> to,
       List<string> ccAddresses,
       List<string> bccAddresses,
       string subject,
       string body,
       CancellationToken cancellationToken)
    {
        var request = new SendEmailRequest
        {
            FromEmailAddress = from,
            Destination = new Destination
            {
                ToAddresses = to,
                CcAddresses = ccAddresses,
                BccAddresses = bccAddresses,
            },
            Content = new EmailContent
            {
                Simple = new Message
                {
                    Subject = new Content { Data = subject },
                    Body = new Body
                    {
                        Html = new Content { Data = body },
                    },
                }
            }
        };

        await _sesClient.SendEmailAsync(request, cancellationToken);
    }
}