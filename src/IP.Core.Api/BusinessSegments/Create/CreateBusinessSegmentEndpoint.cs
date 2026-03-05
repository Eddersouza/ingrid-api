namespace IP.Core.Api.BusinessSegments.Create;

internal class CreateBusinessSegmentEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/business-branches/{businessBranchId}/business-segments", Call)
            .RequireAuthorization(BusinessSegmentClaim.CanCreate.Name)
            .MapEndpointProduces<CreateBusinessSegmentResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Segmento de Negócio",
                "Cria novo registro de Segmento de Negócio",
                "Segmentos de Negócio");
    }
    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateBusinessSegmentRequest request,
        [FromRoute] Guid businessBranchId,
        [FromServices] ICommandHandler<CreateBusinessSegmentCommand, CreateBusinessSegmentResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        request.BusinessBranchId = businessBranchId;

        var result = await commandHandler.Handle(new CreateBusinessSegmentCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
