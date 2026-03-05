namespace IP.AccIPInfo.Api.AccountSubscriptions.UpdateAccountSubscription;

internal class UpdateAccountSubscriptionEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/account-subscriptions/{id:guid}", Call)
            .RequireAuthorization(AccountSubscriptionClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateAccountSubscriptionResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Plano",
                "Altera registro de Plano",
                "Planos");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateAccountSubscriptionRequest request,
        [FromServices] ICommandHandler<UpdateAccountSubscriptionCommand, UpdateAccountSubscriptionResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateAccountSubscriptionResponse> result =
            await commandHandler.Handle(new UpdateAccountSubscriptionCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
