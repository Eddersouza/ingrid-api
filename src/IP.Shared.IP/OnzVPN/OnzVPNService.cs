namespace IP.Shared.IP.OnzVPN;

public interface IOnzVPNFunctions
{
    Task CloseVPNAsync();

    Task OpenVPNAsync();
}

public sealed class OnzVPNServiceOptions : S3Options
{
    public const string NameKey = "AWS.Infopago.VPN.onz.Certificate";

    public string ObjectKey { get; set; } = null!;
}

internal sealed class OnzVPNService(
    IOptions<OnzVPNServiceOptions> options,
    ILogger<OnzVPNService> _logger,
    IOnzVPNSSHTunnelService _onzVPNSSHTunnelService)
    : IOnzVPNFunctions
{
    private OnzVPNServiceOptions _certificateOptions => options.Value;

    public async Task CloseVPNAsync()
    {
        try
        {
            _logger.LogInformation("[{ServiceName}] - Starting close ONZ vpn.",
              nameof(OnzVPNService));
            await Task.Run(() => _onzVPNSSHTunnelService.StopTunnel());
            _logger.LogInformation("[{ServiceName}] - Closed ONZ vpn.",
              nameof(OnzVPNService));
        }
        catch (Exception exception)
        {
            _logger.LogError("[{ServiceName}] - Failed close ONZ vpn. Error: {StackTrace}",
              nameof(OnzVPNService),
              exception.GetInnerExceptions());
            throw;
        }
    }

    public async Task OpenVPNAsync()
    {
        try
        {
            _logger.LogInformation("[{ServiceName}] - Starting open ONZ vpn.",
              nameof(OnzVPNService));
            S3Helper s3Helper = S3Helper.Create(_certificateOptions);

            var certificateFileMemory = await s3Helper.GetFileInMemory(_certificateOptions.BucketName, _certificateOptions.ObjectKey);

            _onzVPNSSHTunnelService.StartTunnel(certificateFileMemory);

            _logger.LogInformation("[{ServiceName}] - Opened ONZ vpn.",
              nameof(OnzVPNService));
        }
        catch (Exception exception)
        {
            _logger.LogError("[{ServiceName}] - Failed open ONZ vpn. Error: {StackTrace}",
              nameof(OnzVPNService),
              exception.GetInnerExceptions());

            throw;
        }
    }
}