namespace IP.Core.Api.Cities.GetSelectItems;

internal class GetSelectItemsEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/cities/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsCitiesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Cidades como Select Items",
                "Lista registros de Cidade como Select Items",
                "Cidades");
    }
    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsCitiesQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsCitiesQuery, GetSelectItemsCitiesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsCitiesResponse> result =
            await commandHandler.Handle(new GetSelectItemsCitiesQuery(request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
