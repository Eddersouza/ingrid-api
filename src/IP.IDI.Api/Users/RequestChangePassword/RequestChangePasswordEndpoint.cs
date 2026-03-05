namespace IP.IDI.Api.Users.RequestChangePassword;

internal class RequestChangePasswordEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/users/{id:guid}/change-password/request", Call)
            .RequireAuthorization()
            .MapEndpointProduces<RequestChangePasswordResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Trocar senha de Usuário",
                "Inicia processo de troca de senha de Usuário",
                "Usuários", "Auth");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] ICommandHandler<RequestChangePasswordCommand, RequestChangePasswordResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<RequestChangePasswordResponse> result =
            await commandHandler.Handle(new RequestChangePasswordCommand(id), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Accepted, CustomResults.Problem, context, location);
    }
}