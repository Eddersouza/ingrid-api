namespace IP.Shared.Emails;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IAWSServiceEmail, AWSServiceEmail>();
        services.Configure<AWSServiceEmailOptions>(
           configuration.GetSection(AWSServiceEmailOptions.NameKey));
        
        services.AddScoped<ISMTPServiceEmail, SMTPServiceEmail>();
        services.Configure<SMTPServiceEmailOptions>(
           configuration.GetSection(SMTPServiceEmailOptions.NameKey));

        return services;
    }
}
