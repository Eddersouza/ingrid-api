namespace IP.Shared.Api.Documents.Returns;

internal sealed class BadRequestResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{

    public BadRequestResponseTransformer() :
        base(StatusCodes.Status400BadRequest)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["400"] = CreateSchemaResponse();

        return Task.CompletedTask;
    }      
}