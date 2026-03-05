namespace IP.Com.Api.EmailsSchedule.RequestChangePassword;

internal class RequestChangePasswordIntegrationEventHandler(
    IComUoW _unitOfWork,
    IOptions<AuthConfigurationOptions> _securityOptions,
    IOptions<ApiEmailOptions> _emailOptions) :
    IIntegrationEventHandler<RequestedChangePasswordIntegrationEvent>
{
    private readonly IEmailScheduleRepository _emailScheduleRepository =
        _unitOfWork.GetRepository<IEmailScheduleRepository>();
    
    private readonly AuthConfigurationOptions authOptions = _securityOptions.Value;

    private readonly ApiEmailOptions emailOptions = _emailOptions.Value;

    private string _templateBasePath = string.Empty;

    public async Task Handle(
        RequestedChangePasswordIntegrationEvent domainEvent,
        CancellationToken cancellationToken)
    {
        string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

        var dir = new DirectoryInfo(assemblyLocation);

        _templateBasePath = Path.Combine(dir.FullName, "EmailTemplates", "ChangePassword.html");

        if (!File.Exists(_templateBasePath))
            throw new FileNotFoundException($"Template não encontrado: {_templateBasePath}");

        string templateContent = File.ReadAllText(_templateBasePath);

        var emailData = new
        {
            user = domainEvent.User,
            systemUrl = $"{authOptions.UrlPageChangePassword}?n={domainEvent.Id}&k={domainEvent.TokenHash}",
            supportEmail = emailOptions.Support,
            requestedByAdm = domainEvent.RequestedByAdm
        };

        HandlebarsTemplate<object, object> template = Handlebars.Compile(templateContent);

        var html = template(emailData);

        var emailSchedule = EmailSchedule.Create(
            emailOptions.DefaultSender,
            domainEvent.Email,
            "Troca de senha",
            html);

        await _emailScheduleRepository.Create(emailSchedule);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}