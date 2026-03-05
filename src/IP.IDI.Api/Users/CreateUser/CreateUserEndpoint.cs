using IP.Shared.Abstractions.Auths.Claims;

namespace IP.IDI.Api.Users.CreateUser;

internal sealed class CreateUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", Call)
            .RequireAuthorization(UserClaim.CanCreate.Name)
            .MapEndpointProduces<CreateUserResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Usuário",
                "Cria novo registro de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateUserRequest request,
        [FromServices] ICommandHandler<CreateUserCommand, CreateUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateUserResponse> result =
            await commandHandler.Handle(new CreateUserCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}