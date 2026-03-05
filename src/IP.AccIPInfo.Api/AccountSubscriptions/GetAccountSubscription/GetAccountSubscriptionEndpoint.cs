namespace IP.AccIPInfo.Api.AccountSubscriptions.GetAccountSubscription;

internal sealed class GetAccountSubscriptionEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/account-subscriptions/{id:guid}", Call)
            .RequireAuthorization(AccountSubscriptionClaim.CanRead.Name)
            .MapEndpointProduces<GetAccountSubscriptionResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Planos",
                "Busca registro de Plano",
                "Planos");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetAccountSubscriptionQuery, GetAccountSubscriptionResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetAccountSubscriptionResponse> result =
            await commandHandler.Handle(new GetAccountSubscriptionQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}