using Microsoft.Extensions.Options;

namespace IP.Shared.Api.Documents.Geral;

internal sealed class ApiDocumentTransformer(
    IOptions<ApiDocumentInfoOptions> options) : 
    IOpenApiDocumentTransformer
{
    private readonly ApiDocumentInfoOptions apiDocumentInfoOptions = options.Value;
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info = new()
        {
            Title = apiDocumentInfoOptions.Title,
            Version = context.DocumentName,
            Description = apiDocumentInfoOptions.Description,
            Contact = new()
            {
                Name = apiDocumentInfoOptions.Contact.Name,
                Email = apiDocumentInfoOptions.Contact.Email
            }
        };

        List<OpenApiTag> tagList = [];

        foreach (var tag in apiDocumentInfoOptions.Tags)
        {
            tagList.Add(new() { Name = tag.Name, Description = tag.Description });
        }

        document.Tags = tagList;
        
        return Task.CompletedTask;
    }
}