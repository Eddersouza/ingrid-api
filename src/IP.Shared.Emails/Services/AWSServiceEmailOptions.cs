namespace IP.Shared.Emails.Services;

internal sealed class AWSServiceEmailOptions
{
    public const string NameKey = "Email.AWS";

    public string AccessKey { get; set; } = string.Empty;
    public bool Enabled { get; set; } = false;
    public string Region { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string ServiceUrl { get; set; } = string.Empty;
}