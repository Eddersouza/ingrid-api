using IP.Shared.IP.OnzVPN;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IP.Shared.IP;

public static class DependencyInjection
{
    public static IServiceCollection AddOnzVPN(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OnzVPNServiceOptions>(
           configuration.GetSection(OnzVPNServiceOptions.NameKey));

        services.Configure<OnzVPNSSHTunnelOptions>(
          configuration.GetSection(OnzVPNSSHTunnelOptions.NameKey));

        services.AddScoped<IOnzVPNFunctions, OnzVPNService>();
        services.AddScoped<IOnzVPNSSHTunnelService, OnzVPNSSHTunnelService>();

        return services;
    }
}