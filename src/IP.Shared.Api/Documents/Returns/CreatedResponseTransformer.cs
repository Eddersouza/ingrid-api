namespace IP.Shared.Api.Documents.Returns;

internal sealed class CreatedResponseTransformer :
    ResponseTransformerBase,
    IOpenApiOperationTransformer
{
    public CreatedResponseTransformer() :
        base(StatusCodes.Status201Created)
    { }

    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (!IsCurrentStatusCode(context)) return Task.CompletedTask;
        operation.Responses["201"].Description = StatusCodesDescription[StatusCode];

        return Task.CompletedTask;
    }
}