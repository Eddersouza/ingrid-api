namespace IP.Core.Api.Cities.DeleteCity;

internal class DeleteCityEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/cities/{id:guid}", Call)
            .RequireAuthorization(CityClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteCityResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Cidade",
                "Remover registro de Cidade",
                "Cidades");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteCityRequest request,
        [FromServices] ICommandHandler<DeleteCityCommand, DeleteCityResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteCityResponse> result =
            await commandHandler.Handle(new DeleteCityCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
