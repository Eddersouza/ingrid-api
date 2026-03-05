namespace IP.AccIPInfo.Api.AccountSubscriptions.ActiveAccountSubscription;

internal sealed class ActiveAccountSubscriptionEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/account-subscriptions/active/{id:guid}", Call)
            .RequireAuthorization(AccountSubscriptionClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ActiveAccountSubscriptionResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Plano",
                "Ativa ou Desativa registro de Plano",
                "Planos");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ActiveAccountSubscriptionRequest request,
        [FromServices] ICommandHandler<ActiveAccountSubscriptionCommand, ActiveAccountSubscriptionResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveAccountSubscriptionResponse> result =
            await commandHandler.Handle(new ActiveAccountSubscriptionCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}