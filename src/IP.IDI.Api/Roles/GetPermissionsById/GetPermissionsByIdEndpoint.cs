namespace IP.IDI.Api.Roles.GetPermissionsById;

internal class GetPermissionsByIdByIdEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles/{id:guid}/permissions", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetPermissionsByIdResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Permissões do Perfil",
                "Busca Permissões do Perfil no sistema",
                "Perfis");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetPermissionsByIdQuery, GetPermissionsByIdResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetPermissionsByIdResponse> result =
            await commandHandler.Handle(new GetPermissionsByIdQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
