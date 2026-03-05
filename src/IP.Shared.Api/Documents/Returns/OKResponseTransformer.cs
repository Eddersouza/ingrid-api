namespace IP.Shared.Api.Documents.Returns;

internal sealed class OKResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public OKResponseTransformer() :
        base(StatusCodes.Status200OK)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["200"].Description = StatusCodesDescription[StatusCode];

        return Task.CompletedTask;
    }
}