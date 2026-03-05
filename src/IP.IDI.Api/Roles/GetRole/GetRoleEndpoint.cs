namespace IP.IDI.Api.Roles.GetRole;

internal sealed class GetRoleEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles/{id:guid}", Call)
            .RequireAuthorization(RoleClaim.CanRead.Name)
            .MapEndpointProduces<GetRoleResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Perfil",
                "Busca registro de Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetRoleQuery, GetRoleResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetRoleResponse> result =
            await commandHandler.Handle(new GetRoleQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}