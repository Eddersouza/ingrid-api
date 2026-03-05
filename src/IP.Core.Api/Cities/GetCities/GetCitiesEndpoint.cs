namespace IP.Core.Api.Cities.GetCities;

internal class GetCitiesEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/cities", Call)
            .RequireAuthorization(CityClaim.CanList.Name)
            .MapEndpointProduces<GetCitiesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Cidades",
                "Lista registros de Cidade",
                "Cidades");
    }

    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetCitiesQueryRequest request,
       [FromServices] IQueryHandler<GetCitiesQuery, GetCitiesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetCitiesResponse> result =
            await commandHandler.Handle(new GetCitiesQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
