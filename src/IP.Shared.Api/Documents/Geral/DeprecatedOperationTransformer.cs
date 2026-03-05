namespace IP.Shared.Api.Documents.Geral;

internal sealed class DeprecatedOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        operation.Deprecated = context.Description.ActionDescriptor.EndpointMetadata
            .OfType<DeprecatedEndpointMetadata>()
            .FirstOrDefault() is not null;

        return Task.CompletedTask;
    }
}