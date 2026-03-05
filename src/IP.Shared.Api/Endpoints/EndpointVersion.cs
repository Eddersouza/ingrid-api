namespace IP.Shared.Api.Endpoints;

public class EndpointVersion
{
    public EndpointVersion(int version)
    {
        Version = version;
    }

    private EndpointVersion()
    { }

    public int Version { get; set; } = 1;
    public string VersionPattern => $"v{Version}";
}