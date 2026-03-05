namespace IP.IDI.Api.Users.GetUsers;

internal class GetUsersEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", Call)
            .RequireAuthorization(UserClaim.CanList.Name)
            .MapEndpointProduces<GetUsersResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Usuário",
                "Lista registros de Usuário",
                "Usuários");
    }

    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetUsersQueryRequest request,
       [FromServices] IQueryHandler<GetUsersQuery, GetUsersResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetUsersResponse> result =
            await commandHandler.Handle(new GetUsersQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}