
namespace IP.Shared.IP.OnzVPN;

public sealed class OnzVPNSSHTunnelOptions
{
    public const string NameKey = "IP.VPN.ONZ.SSH";

    public string Host { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public int LocalPort { get; set; }
    public string RemoteHost { get; set; } = string.Empty;
    public int RemotePort { get; set; }
}

public interface IOnzVPNSSHTunnelService : IDisposable
{
    void StartTunnel(Stream _privateKeyStream);

    void StopTunnel();
}

internal sealed class OnzVPNSSHTunnelService(
    IOptions<OnzVPNSSHTunnelOptions> options,
    ILogger<OnzVPNSSHTunnelService> logger) : IOnzVPNSSHTunnelService
{
    private readonly OnzVPNSSHTunnelOptions _options = options.Value;
    private readonly ILogger<OnzVPNSSHTunnelService> _logger = logger;
    private SshClient? _sshClient;
    private ForwardedPortLocal? _forwardedPort;

    public void StartTunnel(Stream _privateKeyStream)
    {
        try
        {
            _logger.LogInformation("[{ServiceName}] - Starting SSH Tunnel. Local Port {LocalPort} to {RemoteHost}:{RemotePort}",
                nameof(OnzVPNSSHTunnelService),
                _options.LocalPort,
                _options.RemoteHost,
                _options.RemotePort);

            var privateKey = new PrivateKeyFile(_privateKeyStream);
            var connectionInfo = new Renci.SshNet.ConnectionInfo(_options.Host, _options.User, [
                    new PrivateKeyAuthenticationMethod(_options.User, privateKey)
                ]);

            _sshClient = new SshClient(connectionInfo);
            _sshClient.Connect();

            _forwardedPort = new ForwardedPortLocal("127.0.0.1", (uint)_options.LocalPort, _options.RemoteHost, (uint)_options.RemotePort);
            _sshClient.AddForwardedPort(_forwardedPort);
            _forwardedPort.Start();

            _logger.LogInformation("[{ServiceName}] - Created SSH Tunnel. Local Port {LocalPort} to {RemoteHost}:{RemotePort}",
                nameof(OnzVPNSSHTunnelService),
                _options.LocalPort,
                _options.RemoteHost,
                _options.RemotePort);
        }
        catch (Exception exception)
        {
            _logger.LogError("[{ServiceName}] - Failed Starting SSH Tunnel. Local Port {LocalPort} to {RemoteHost}:{RemotePort}. Error: {StackTrace}",
                nameof(OnzVPNSSHTunnelService),
                _options.LocalPort,
                _options.RemoteHost,
                _options.RemotePort,
                exception.GetInnerExceptions());
            throw;
        }
    }

    public void StopTunnel()
    {
        try
        {
            if (_forwardedPort != null && _forwardedPort.IsStarted)
            {
                _forwardedPort.Stop();
                _logger.LogInformation("[{ServiceName}] - Stopping SSH Tunnel. Local Port {LocalPort} to {RemoteHost}:{RemotePort}",
                nameof(OnzVPNSSHTunnelService),
                _options.LocalPort,
                _options.RemoteHost,
                _options.RemotePort);
            }

            if (_sshClient != null && _sshClient.IsConnected)
            {
                _sshClient.Disconnect();
                _logger.LogInformation("[{ServiceName}] - SSH Tunnel client disconnected. Local Port {LocalPort} to {RemoteHost}:{RemotePort}",
                nameof(OnzVPNSSHTunnelService),
                _options.LocalPort,
                _options.RemoteHost,
                _options.RemotePort);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("[{ServiceName}] - Failed Stopping SSH Tunnel. Local Port {LocalPort} to {RemoteHost}:{RemotePort}. Error: {StackTrace}",
                nameof(OnzVPNSSHTunnelService),
                _options.LocalPort,
                _options.RemoteHost,
                _options.RemotePort,
                exception.GetInnerExceptions());
        }
    }

    public void Dispose()
    {
        StopTunnel();
        _sshClient?.Dispose();
    }
}