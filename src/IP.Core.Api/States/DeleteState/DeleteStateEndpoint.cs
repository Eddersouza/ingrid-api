namespace IP.Core.Api.States.DeleteState;

internal class DeleteStateEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/states/{id:guid}", Call)
            .RequireAuthorization(StateClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteStateResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Estado",
                "Remover registro de Estado",
                "Estados");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteStateRequest request,
        [FromServices] ICommandHandler<DeleteStateCommand, DeleteStateResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteStateResponse> result =
            await commandHandler.Handle(new DeleteStateCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
