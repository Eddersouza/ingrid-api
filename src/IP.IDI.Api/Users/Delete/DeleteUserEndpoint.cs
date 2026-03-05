namespace IP.IDI.Api.Users.Delete;

internal class DeleteUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{id:guid}", Call)
            .RequireAuthorization(UserClaim.CanDelete.Name)
            .MapEndpointProduces<DeleteUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Usuário",
                "Remover registro de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteUserRequest request,
        [FromServices] ICommandHandler<DeleteUserCommand, DeleteUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteUserResponse> result =
            await commandHandler.Handle(new DeleteUserCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}