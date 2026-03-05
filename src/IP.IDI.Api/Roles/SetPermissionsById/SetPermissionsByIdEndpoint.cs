namespace IP.IDI.Api.Roles.SetPermissionsById;

internal class SetPermissionsByIdEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/roles/{id:guid}/permissions", Call)
            .RequireAuthorization(RoleClaim.CanUpdate.Name)
            .MapEndpointProduces<SetPermissionsByIdResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Permissões Perfil",
                "Altera registro de Permissões do Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] SetPermissionsByIdRequest request,
        [FromServices] ICommandHandler<SetPermissionsByIdCommand, SetPermissionsByIdResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<SetPermissionsByIdResponse> result =
            await commandHandler.Handle(new SetPermissionsByIdCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}