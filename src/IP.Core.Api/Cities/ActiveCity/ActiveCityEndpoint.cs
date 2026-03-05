namespace IP.Core.Api.Cities.ActiveCity;

internal sealed class ActiveCityEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/cities/active/{id:guid}", Call)
            .RequireAuthorization(CityClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ActiveCityResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Cidade",
                "Ativa ou Desativa registro de Cidade",
                "Cidades");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ActiveCityRequest request,
        [FromServices] ICommandHandler<ActiveCityCommand, ActiveCityResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveCityResponse> result =
            await commandHandler.Handle(new ActiveCityCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
