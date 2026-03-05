namespace IP.IDI.Api.AppGuides.ViewLinkUser;

internal class ViewLinkUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/app-guides/link-view/{linkId}/users", Call)
            .RequireAuthorization()
            .MapEndpointProduces<ViewLinkUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Verificar Usuários que visualizaram o Guia do Sistema",
                "Lista os usuários que visualizaram o Guia do Sistema de acordo com o linkId e filtros informados.",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] string linkId,
        [AsParameters] ViewLinkUserQueryRequest queryRequest,
        [FromServices] IQueryHandler<ViewLinkUserQuery, ViewLinkUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ViewLinkUserResponse> result =
            await commandHandler.Handle(
                new ViewLinkUserQuery(linkId, queryRequest), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
