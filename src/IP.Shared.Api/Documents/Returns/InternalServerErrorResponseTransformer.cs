namespace IP.Shared.Api.Documents.Returns;

internal class InternalServerErrorResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public InternalServerErrorResponseTransformer() :
        base(StatusCodes.Status500InternalServerError)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["500"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }
}