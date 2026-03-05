namespace IP.AccIPInfo.Api.IntegratorSystems.DeleteSystem;

internal class DeleteSystemEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/integrator-systems/{id:guid}", Call)
            .RequireAuthorization(IntegratorSystemClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteSystemResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Sistema",
                "Remover registro de Sistema",
                "Sistemas");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteSystemRequest request,
        [FromServices] ICommandHandler<DeleteSystemCommand, DeleteSystemResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteSystemResponse> result =
            await commandHandler.Handle(new DeleteSystemCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
