namespace IP.Core.Api.States.GetStates;

internal class GetStatesEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/states", Call)
            .RequireAuthorization(StateClaim.CanList.Name)
            .MapEndpointProduces<GetStatesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Estados",
                "Lista registros de Estado",
                "Estados");
    }

    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetStatesQueryRequest request,
       [FromServices] IQueryHandler<GetStatesQuery, GetStatesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetStatesResponse> result =
            await commandHandler.Handle(new GetStatesQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }

}
