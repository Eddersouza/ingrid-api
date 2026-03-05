namespace IP.IDI.Api.AppGuides.GetById;

internal sealed class GetAppGuideEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/app-guides/{id:guid}", Call)
            .RequireAuthorization(AppGuideClaim.CanRead.Name)
            .MapEndpointProduces<GetAppGuideResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Guia do Sistema",
                "Busca registro de Guia do Sistema",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetAppGuideQuery, GetAppGuideResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetAppGuideResponse> result =
            await commandHandler.Handle(new GetAppGuideQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}