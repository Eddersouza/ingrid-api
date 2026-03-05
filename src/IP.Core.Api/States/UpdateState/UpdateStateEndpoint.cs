namespace IP.Core.Api.States.UpdateState;

internal class UpdateStateEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/states/{id:guid}", Call)
            .RequireAuthorization(StateClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateStateResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Estado",
                "Altera registro de Estado",
                "Estados");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateStateRequest request,
        [FromServices] ICommandHandler<UpdateStateCommand, UpdateStateResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateStateResponse> result =
            await commandHandler.Handle(new UpdateStateCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
