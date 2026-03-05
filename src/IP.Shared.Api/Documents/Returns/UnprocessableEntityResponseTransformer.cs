namespace IP.Shared.Api.Documents.Returns;

internal class UnprocessableEntityResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public UnprocessableEntityResponseTransformer() :
        base(StatusCodes.Status422UnprocessableEntity)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["422"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }
}