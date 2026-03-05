namespace IP.IDI.Api.Roles.ActiveRole;

internal sealed class ActiveRoleEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/roles/active/{id:guid}", Call)
            .RequireAuthorization(RoleClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ActiveRoleResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Perfil",
                "Ativa ou Desativa registro de Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ActiveRoleRequest request,
        [FromServices] ICommandHandler<ActiveRoleCommand, ActiveRoleResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveRoleResponse> result =
            await commandHandler.Handle(new ActiveRoleCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}