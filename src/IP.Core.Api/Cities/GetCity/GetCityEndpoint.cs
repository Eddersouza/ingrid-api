namespace IP.Core.Api.Cities.GetCity;

internal sealed class GetCityEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/cities/{id:guid}", Call)
            .RequireAuthorization(CityClaim.CanRead.Name)
            .MapEndpointProduces<GetCityResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Cidades",
                "Busca registro de Cidade",
                "Cidades");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetCityQuery, GetCityResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetCityResponse> result =
            await commandHandler.Handle(new GetCityQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
