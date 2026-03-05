namespace IP.IDI.Api.Users.Confirm;

internal sealed class ConfirmUserCommandEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/{id:guid}/confirm", Call)
            .MapEndpointProduces<ConfirmUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Confirmar Usuário",
                "Confirma registro de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ConfirmUserRequest request,
        [FromServices] ICommandHandler<ConfirmUserCommand, ConfirmUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ConfirmUserResponse> result =
            await commandHandler.Handle(new ConfirmUserCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}