namespace IP.IDI.Api.AppGuides.Get;

internal sealed class GetAppGuidesEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/app-guides", Call)
            .RequireAuthorization(AppGuideClaim.CanList.Name)
            .MapEndpointProduces<GetAppGuidesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Guia do Sistema",
                "Lista registros de Guia do Sistema",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetAppGuidesQueryRequest request,
       [FromServices] IQueryHandler<GetAppGuidesQuery, GetAppGuidesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetAppGuidesResponse> result =
            await commandHandler.Handle(new GetAppGuidesQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}