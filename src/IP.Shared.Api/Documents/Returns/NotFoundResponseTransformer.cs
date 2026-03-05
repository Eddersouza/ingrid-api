namespace IP.Shared.Api.Documents.Returns;

internal class NotFoundResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public NotFoundResponseTransformer() :
        base(StatusCodes.Status404NotFound)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["404"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }
}