using IP.Shared.Abstractions.Sessions;
using IP.Shared.EndpointModules.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace IP.Shared.EndpointModules;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpointModuleServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuthHeaderHandler>();
        services.Configure<EndpointModuleOptions>(
            configuration.GetSection(EndpointModuleOptions.NameKey));

        EndpointModuleOptions endpointOptions =
            configuration.GetSection(EndpointModuleOptions.NameKey)
            .Get<EndpointModuleOptions>()
            ?? throw new ArgumentNullException(
                nameof(configuration),
                "AuthConfigurationOptions section is missing or invalid.");

        services.AddRefitClient<IEmployeeEndpoint>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(endpointOptions.CoreUrl);
        }).AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }
}

public class AuthHeaderHandler(
    ICurrentUserInfo _currentUserInfo) : DelegatingHandler
{
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string jwtToken = _currentUserInfo.GetJwtToken();
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

        return await base.SendAsync(request, cancellationToken);
    }
}