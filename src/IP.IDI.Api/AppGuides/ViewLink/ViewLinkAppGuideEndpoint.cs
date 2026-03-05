namespace IP.IDI.Api.AppGuides.ViewLink;

internal class ViewLinkAppGuideEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/app-guides/link-view/{linkId}", Call)
            .RequireAuthorization()
            .MapEndpointProduces<ViewLinkAppGuideResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Verificar Visualização de Guia do Sistema",
                "Verifica se o usuário visualizou o guia do sistema baseado no linkId passado como parâmetro",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] string linkId,
        [FromServices] IQueryHandler<ViewLinkAppGuideQuery, ViewLinkAppGuideResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ViewLinkAppGuideResponse> result =
            await commandHandler.Handle(new ViewLinkAppGuideQuery(linkId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
