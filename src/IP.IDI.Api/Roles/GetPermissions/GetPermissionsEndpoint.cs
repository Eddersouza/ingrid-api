namespace IP.IDI.Api.Roles.GetPermissions;

internal class GetPermissionsEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles/permissions", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetPermissionsResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Permissões",
                "Busca Permissões do sistema",
                "Perfis");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromServices] IQueryHandler<GetPermissionsQuery, GetPermissionsResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetPermissionsResponse> result =
            await commandHandler.Handle(new GetPermissionsQuery(), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
