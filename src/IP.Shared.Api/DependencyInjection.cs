namespace IP.Shared.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiOptions(
      this IServiceCollection services,
      IConfiguration configuration)
    {
        services.Configure<ApiEmailOptions>(
            configuration.GetSection(ApiEmailOptions.NameKey));

        return services;
    }
    }
