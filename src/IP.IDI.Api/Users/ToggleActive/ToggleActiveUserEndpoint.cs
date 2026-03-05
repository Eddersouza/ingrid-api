namespace IP.IDI.Api.Users.ToggleActive;

internal sealed class ToggleActiveUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/active/{id:guid}", Call)
            .RequireAuthorization(UserClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ToggleActiveUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Usuário",
                "Ativa ou Desativa registro de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ToggleActiveUserRequest request,
        [FromServices] ICommandHandler<ToggleActiveUserCommand, ToggleActiveUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ToggleActiveUserResponse> result =
            await commandHandler.Handle(new ToggleActiveUserCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}