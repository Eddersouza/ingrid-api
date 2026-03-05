namespace IP.IDI.Api.Roles.DeleteRole;

internal class DeleteRoleEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/roles/{id:guid}", Call)
            .RequireAuthorization(RoleClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteRoleResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Perfil",
                "Remover registro de Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteRoleRequest request,
        [FromServices] ICommandHandler<DeleteRoleCommand, DeleteRoleResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteRoleResponse> result =
            await commandHandler.Handle(new DeleteRoleCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}