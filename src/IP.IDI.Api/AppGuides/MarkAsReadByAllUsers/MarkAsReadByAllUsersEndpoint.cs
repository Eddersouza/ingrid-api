namespace IP.IDI.Api.AppGuides.MarkAsReadByAllUsers;

internal sealed class MarkAsReadByAllUsersEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/app-guides/link-view/{linkId}/users", Call)
            .RequireAuthorization()
            .MapEndpointProduces<MarkAsReadByAllUsersResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Marcar todas  usuario como não visualizado.",
                "Marca todos os Usuários como não visualizado na Guia do Sistema.",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] string linkId,
        [FromServices] ICommandHandler<MarkAsReadByAllUsersCommand, MarkAsReadByAllUsersResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<MarkAsReadByAllUsersResponse> result =
            await commandHandler.Handle(
                new MarkAsReadByAllUsersCommand(linkId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}