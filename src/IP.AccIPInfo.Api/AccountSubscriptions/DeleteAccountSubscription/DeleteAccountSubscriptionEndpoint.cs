namespace IP.AccIPInfo.Api.AccountSubscriptions.DeleteAccountSubscription;

internal class DeleteAccountSubscriptionEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/account-subscriptions/{id:guid}", Call)
            .RequireAuthorization(AccountSubscriptionClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteAccountSubscriptionResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Plano",
                "Remover registro de Plano",
                "Planos");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteAccountSubscriptionRequest request,
        [FromServices] ICommandHandler<DeleteAccountSubscriptionCommand, DeleteAccountSubscriptionResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteAccountSubscriptionResponse> result =
            await commandHandler.Handle(new DeleteAccountSubscriptionCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}