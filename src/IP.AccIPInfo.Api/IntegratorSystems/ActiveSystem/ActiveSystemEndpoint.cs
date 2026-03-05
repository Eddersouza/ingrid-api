namespace IP.AccIPInfo.Api.IntegratorSystems.ActiveSystem;

internal class ActiveSystemEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/integrator-systems/active/{id:guid}", Call)
            .RequireAuthorization(IntegratorSystemClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ActiveSystemResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Sistema Integrador",
                "Ativa ou Desativa registro de Sistema Integrador",
                "Sistemas");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ActiveSystemRequest request,
        [FromServices] ICommandHandler<ActiveSystemCommand, ActiveSystemResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveSystemResponse> result =
            await commandHandler.Handle(new ActiveSystemCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
