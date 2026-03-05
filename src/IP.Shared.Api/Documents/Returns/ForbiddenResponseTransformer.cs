namespace IP.Shared.Api.Documents.Returns;

internal class ForbiddenResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public ForbiddenResponseTransformer() :
        base(StatusCodes.Status403Forbidden)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["403"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }
}