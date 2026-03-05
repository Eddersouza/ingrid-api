namespace IP.Shared.Api.Options;

public sealed class ApiEmailOptions
{
    public const string NameKey = "Api.Email";
    public string Contact { get; set; } = string.Empty;
    public string NoReply { get; set; } = string.Empty;
    public string Support { get; set; } = string.Empty;
    public string DefaultSender { get; set; } = string.Empty;
}