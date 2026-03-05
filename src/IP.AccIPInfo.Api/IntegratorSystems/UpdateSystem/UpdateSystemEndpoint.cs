namespace IP.AccIPInfo.Api.IntegratorSystems.UpdateSystem;

internal class UpdateSystemEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/integrator-systems/{id:guid}", Call)
            .RequireAuthorization(IntegratorSystemClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateSystemResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Sistema Integrador",
                "Altera registro de Sistema Integrador",
                "Sistemas");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateSystemRequest request,
        [FromServices] ICommandHandler<UpdateSystemCommand, UpdateSystemResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateSystemResponse> result =
            await commandHandler.Handle(new UpdateSystemCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}