namespace IP.IDI.Api.Users.Authenticate;

internal class AuthenticateUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/authenticate", Call)
           .MapEndpointProduces<AuthenticateUserResponse>()
           .MapEndpointVersions(1)
           .MapEndpointDescription(
               "Autenticar Usuário",
               "Faz o login do Usuário e recebe o token do usuário logado",
               "Auth","Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] AuthenticateUserRequest request,
        [FromServices] ICommandHandler<AuthenticateUserCommand, AuthenticateUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<AuthenticateUserResponse> result =
            await commandHandler.Handle(new AuthenticateUserCommand(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
