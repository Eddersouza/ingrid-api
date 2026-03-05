namespace IP.IDI.Api.Users.GetUser;

internal sealed class GetUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:guid}", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetUserResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Buscar Usuário",
                "Busca registro de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetUserQuery, GetUserResponse> queryHandler,
        CancellationToken cancellationToken)
    {
        Result<GetUserResponse> result =
            await queryHandler.Handle(new GetUserQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}