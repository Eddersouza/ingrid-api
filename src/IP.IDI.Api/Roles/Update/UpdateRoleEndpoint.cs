namespace IP.IDI.Api.Roles.Update;

internal class UpdateRoleEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/roles/{id:guid}", Call)
            .RequireAuthorization(RoleClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateRoleResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Perfil",
                "Altera registro de Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateRoleRequest request,
        [FromServices] ICommandHandler<UpdateRoleCommand, UpdateRoleResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateRoleResponse> result =
            await commandHandler.Handle(new UpdateRoleCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}