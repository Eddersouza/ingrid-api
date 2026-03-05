namespace IP.AccIPInfo.Api.IntegratorSystems.GetSystems;

internal class GetSystemsEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/integrator-systems", Call)
            .RequireAuthorization(IntegratorSystemClaim.CanList.Name)
            .MapEndpointProduces<GetSystemsResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Sistemas Integradores",
                "Lista registros de Sistema Integrador",
                "Sistemas");
    }
    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSystemsQueryRequest request,
       [FromServices] IQueryHandler<GetSystemsQuery, GetSystemsResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSystemsResponse> result =
            await commandHandler.Handle(new GetSystemsQuery(request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
