namespace IP.Shared.Api;

public static class CustomResults
{
    public static IResult Problem(Result result, HttpContext context)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException(
                "Cannot return success and a problem detail");

        return Results.Problem(title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error),
            statusCode: GetStatusCode(result.Error.Type),
            instance: $"{context.Request.Method} {context.Request.Path}",
            extensions: GetErrors(result));
    }

    private static string GetDetail(Error error) =>
        error.Type switch
        {
            ErrorType.Validation or
                ErrorType.Problem or
                ErrorType.NotFound or
                ErrorType.Conflict or
                ErrorType.Forbidden or
                ErrorType.UnprocessableEntity => error.Description,
            ErrorType.Failure or
                _ => "An unexpected error occurred"
        };

    private static Dictionary<string, object?>? GetErrors(Result result)
    {
        if (result.Error is not ErrorValidation validationError)
        {
            return null;
        }

        return new Dictionary<string, object?>
        {
            { "errors", validationError.Errors.Select(e => new { e.Code, e.Description }) }
        };
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation or
                ErrorType.Problem => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.UnprocessableEntity => StatusCodes.Status422UnprocessableEntity,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Failure or
                _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Error error) =>
        error.Type switch
        {
            ErrorType.Validation or
                    ErrorType.Problem or
                    ErrorType.NotFound or
                    ErrorType.Conflict or
                    ErrorType.Forbidden or
                    ErrorType.UnprocessableEntity => error.Code,
            ErrorType.Failure or
                    _ => "Server Failure"
        };

    private static string GetType(Error error) =>
        error.Type switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.Problem => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            ErrorType.UnprocessableEntity => "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2",
            ErrorType.Forbidden => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            ErrorType.Failure or
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
}