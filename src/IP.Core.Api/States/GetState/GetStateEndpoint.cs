namespace IP.Core.Api.States.GetState;

internal sealed class GetStateEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/states/{id:guid}", Call)
            .RequireAuthorization(StateClaim.CanRead.Name)
            .MapEndpointProduces<GetStateResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Estado",
                "Busca registro de Estado",
                "Estados");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetStateQuery, GetStateResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetStateResponse> result =
            await commandHandler.Handle(new GetStateQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}