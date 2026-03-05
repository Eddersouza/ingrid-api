namespace IP.Shared.Abstractions.Sessions;

public static class SessionExtension
{
    public static IServiceCollection AddCurrentSession(this IServiceCollection services)
    {
        services.AddScoped<ICurrentSessionProvider, CurrentSessionProvider>();
        services.AddScoped<ICurrentUserInfo, CurrentUserInfo>();

        return services;
    }
}
