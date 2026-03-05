namespace IP.AccIPInfo.Api.AccountSubscriptions.GetAccountSubscriptions;

internal class GetAccountSubscriptionsEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/account-subscriptions", Call)
            .RequireAuthorization(AccountSubscriptionClaim.CanList.Name)
            .MapEndpointProduces<GetAccountSubscriptionsResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Planos",
                "Lista registros de Plano",
                "Planos");
    }

    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetAccountSubscriptionsQueryRequest request,
       [FromServices] IQueryHandler<GetAccountSubscriptionsQuery, GetAccountSubscriptionsResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetAccountSubscriptionsResponse> result =
            await commandHandler.Handle(new GetAccountSubscriptionsQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}