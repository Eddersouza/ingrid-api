namespace IP.IDI.Api.Roles.GetRoles;

internal sealed class GetRolesEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles", Call)
            .RequireAuthorization(RoleClaim.CanList.Name)
            .MapEndpointProduces<GetRolesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Perfil",
                "Lista registros de Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetRolesQueryRequest request,
       [FromServices] IQueryHandler<GetRolesQuery, GetRolesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetRolesResponse> result =
            await commandHandler.Handle(new GetRolesQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}