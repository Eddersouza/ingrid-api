namespace IP.IDI.Api.Users.Update;

internal class UpdateUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id:guid}", Call)
            .RequireAuthorization(UserClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Usuário",
                "Altera registro de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateUserRequest request,
        [FromServices] ICommandHandler<UpdateUserCommand, UpdateUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateUserResponse> result =
            await commandHandler.Handle(new UpdateUserCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
