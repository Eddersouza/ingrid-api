namespace IP.Shared.Api.Documents.Returns;

internal class UnauthorizedResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public UnauthorizedResponseTransformer() :
        base(StatusCodes.Status401Unauthorized)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["401"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }
}