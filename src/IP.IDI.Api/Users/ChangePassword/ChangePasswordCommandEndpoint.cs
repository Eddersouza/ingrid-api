namespace IP.IDI.Api.Users.ChangePassword;

internal sealed class ChangePasswordCommandEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/{id:guid}/change-password/set", Call)
            .MapEndpointProduces<ChangePasswordResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Trocar senha Usuário",
                "Troca a senha de Usuário",
                "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ChangePasswordRequest request,
        [FromServices] ICommandHandler<ChangePasswordCommand, ChangePasswordResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ChangePasswordResponse> result =
            await commandHandler.Handle(new ChangePasswordCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}