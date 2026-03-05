namespace IP.Shared.Api;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private const string GetDefaultMessageError =
       @"Estamos enfrentando um problema técnico, mas já estamos trabalhando para resolver. Por favor, tente novamente em alguns instantes.";

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Exception occurred: {Message}",
            exception.GetInnerExceptions());

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = GetDefaultMessageError,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}{httpContext.Request.QueryString}"
        };

        httpContext.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}