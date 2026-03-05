namespace IP.Shared.Api.Documents.Returns;

internal class ConflictResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public ConflictResponseTransformer() :
        base(StatusCodes.Status409Conflict)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["409"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }
}