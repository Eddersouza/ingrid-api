namespace IP.Core.Api.Cities.UpdateCity;

internal class UpdateCityEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/cities/{id:guid}", Call)
            .RequireAuthorization(CityClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateCityResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Cidade",
                "Altera registro de Cidade",
                "Cidades");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateCityRequest request,
        [FromServices] ICommandHandler<UpdateCityCommand, UpdateCityResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateCityResponse> result =
            await commandHandler.Handle(new UpdateCityCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
