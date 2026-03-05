namespace IP.AccIPInfo.Api.IntegratorSystems.GetSelect;

internal sealed class GetSelectItemsIntegratorSystemEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/integrator-systems/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsIntegratorSystemResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Sistemas como Select Items",
                "Lista registros de Sistemas como Select Items",
                "Sistemas");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsIntegratorSystemQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsIntegratorSystemQuery, GetSelectItemsIntegratorSystemResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsIntegratorSystemResponse> result =
            await commandHandler.Handle(new GetSelectItemsIntegratorSystemQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}