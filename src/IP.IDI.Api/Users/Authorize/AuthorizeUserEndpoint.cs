namespace IP.IDI.Api.Users.Authorize;

internal class AuthorizeUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/authorize", Call)
           .MapEndpointProduces<AuthorizeUserResponse>()
           .RequireAuthorization()
           .MapEndpointVersions(1)
           .MapEndpointDescription(
               "Autorização de Usuário",
               "Retorna lista de Autorizações de Usuário",
               "Auth", "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromServices] ICommandHandler<AuthorizeUserCommand, AuthorizeUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<AuthorizeUserResponse> result =
            await commandHandler.Handle(new AuthorizeUserCommand(), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}