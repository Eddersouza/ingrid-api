namespace IP.IDI.Api.AppGuides.MarkAsReadByUser;

internal class MarkAsReadByUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/app-guides/link-view/{linkId}/users/view", Call)
            .RequireAuthorization()
            .MapEndpointProduces<MarkAsReadByUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Marcar usuario como visualizado.",
                "Marca o Usuário como visualizado na Guia do Sistema.",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] string linkId,
        [FromBody] MarkAsReadByUserRequest queryRequest,
        [FromServices] ICommandHandler<MarkAsReadByUserCommand, MarkAsReadByUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<MarkAsReadByUserResponse> result =
            await commandHandler.Handle(
                new MarkAsReadByUserCommand(linkId, queryRequest), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}