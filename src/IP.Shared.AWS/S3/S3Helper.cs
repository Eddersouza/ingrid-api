namespace IP.Shared.AWS.S3;

public class S3Helper
{
    private readonly AmazonS3Client _s3Client;

    private S3Helper(S3Options options)
    {
        if (string.IsNullOrEmpty(options.Url))
        {
            _s3Client = new AmazonS3Client(
            options.AccessKeyId,
            options.SecretAccessKey,
            RegionEndpoint.GetBySystemName(options.Region));
        }
        else
        {
            AmazonS3Config config = new()
            {
                ServiceURL = options.Url,
                ForcePathStyle = true,
                AuthenticationRegion = options.Region,
                UseHttp = true
            };

            _s3Client = new AmazonS3Client(
                options.AccessKeyId,
                options.SecretAccessKey,
                config);
        }
    }

    public static S3Helper Create(S3Options options) => new(options);

    public async Task<MemoryStream> GetFileInMemory(string bucket, string key)
    {
        var stream = new MemoryStream();
        GetObjectResponse response = await _s3Client.GetObjectAsync(bucket, key);

        await response.ResponseStream.CopyToAsync(stream);
        stream.Position = 0;

        return stream;
    }
}