namespace IP.Shared.Security;

public class AuthConfigurationOptions
{
    public const string NameKey = "AuthConfigurationOptions";

    public string Audience { get; set; } = string.Empty;
    public int DefaultLockoutTimeSpanMinutes { get; set; } = 0;
    public int ExpirationInMinutes { get; set; } = 0;
    public string Issuer { get; set; } = string.Empty;
    public int MaxFailedAccessAttempts { get; set; } = 0;
    public string SecretKey { get; set; } = string.Empty;
    public string UrlPageChangePassword { get; set; } = string.Empty;
    public string UrlPageConfirmEmail { get; set; } = string.Empty;
}