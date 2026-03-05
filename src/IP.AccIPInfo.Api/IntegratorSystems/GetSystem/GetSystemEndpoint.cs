namespace IP.AccIPInfo.Api.IntegratorSystems.GetSystem;

internal class GetSystemEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/integrator-systems/{id:guid}", Call)
            .RequireAuthorization(IntegratorSystemClaim.CanRead.Name)
            .MapEndpointProduces<GetSystemResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Sistema Integrador",
                "Busca registro de Sistema Integrador",
                "Sistemas");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetSystemQuery, GetSystemResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetSystemResponse> result =
            await commandHandler.Handle(new GetSystemQuery(id), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
