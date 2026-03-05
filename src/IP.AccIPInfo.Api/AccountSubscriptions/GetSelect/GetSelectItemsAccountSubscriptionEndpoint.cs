namespace IP.AccIPInfo.Api.AccountSubscriptions.GetSelect;

internal sealed class GetSelectItemsAccountSubscriptionEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/account-subscriptions/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsAccountSubscriptionResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Planos como Select Items",
                "Lista registros de Planos como Select Items",
                "Planos");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsAccountSubscriptionQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsAccountSubscriptionQuery, GetSelectItemsAccountSubscriptionResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsAccountSubscriptionResponse> result =
            await commandHandler.Handle(new GetSelectItemsAccountSubscriptionQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}