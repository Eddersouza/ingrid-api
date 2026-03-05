namespace IP.Shared.AWS.S3;

public abstract class S3Options
{
    public string AccessKeyId { get; set; } = string.Empty;
    public string SecretAccessKey { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}