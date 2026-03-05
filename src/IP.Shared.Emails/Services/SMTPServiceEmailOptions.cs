namespace IP.Shared.Emails.Services;

internal sealed class SMTPServiceEmailOptions
{
    public const string NameKey = "Email.SMTP";
    public bool Enabled { get; set; } = false;
    public bool EnableSsl { get; set; } = true;
    public string Host { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
}