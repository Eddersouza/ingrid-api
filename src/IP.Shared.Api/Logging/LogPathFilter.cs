namespace IP.Shared.Api.Logging;

public class LogPathFilter(ILogger<LogPathFilter> logger) : IEndpointFilter
{
    private readonly ILogger<LogPathFilter> _logger = logger;

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;
        _logger.LogInformation(
            "HTTP {Method} {Path}{Query}",
            request.Method,
            request.Path,
            request.QueryString);

        return await next(context);
    }
}

public static class LogPathFilterExtensions
{
    public static IServiceCollection AddLogPathFilter(
        this IServiceCollection services) =>
            services.AddTransient<LogPathFilter>();
}