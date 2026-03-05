namespace IP.Shared.Api.Logging;

public class AWSLogOptions
{
    public const string NameKey = "AWS.Log.Options";
    public string AccessKey { get; set; } = string.Empty;
    public string LogGroupName { get; set; } = string.Empty;
    public string LogStreamPrefix { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string ServiceUrl { get; set; } = string.Empty;
}