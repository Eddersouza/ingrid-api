namespace IP.IDI.Api.AppGuides.ToggleActive;

internal sealed class ToggleActiveAppGuideEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/app-guides/active/{id:guid}", Call)
            .RequireAuthorization(AppGuideClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ToggleActiveAppGuideResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Guia do Sistema",
                "Ativa ou Desativa registro de Guia do Sistema",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ToggleActiveAppGuideRequest request,
        [FromServices] ICommandHandler<ToggleActiveAppGuideCommand, ToggleActiveAppGuideResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ToggleActiveAppGuideResponse> result =
            await commandHandler.Handle(new ToggleActiveAppGuideCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}