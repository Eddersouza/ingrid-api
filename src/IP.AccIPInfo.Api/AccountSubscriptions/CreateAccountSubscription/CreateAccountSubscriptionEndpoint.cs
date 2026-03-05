namespace IP.AccIPInfo.Api.AccountSubscriptions.CreateAccountSubscription;

internal class CreateAccountSubscriptionEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/account-subscriptions", Call)
            .RequireAuthorization(AccountSubscriptionClaim.CanCreate.Name)
            .MapEndpointProduces<CreateAccountSubscriptionResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Plano",
                "Cria novo registro de Plano",
                "Planos");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateAccountSubscriptionRequest request,
        [FromServices] ICommandHandler<CreateAccountSubscriptionCommand, CreateAccountSubscriptionResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateAccountSubscriptionResponse> result =
            await commandHandler.Handle(new CreateAccountSubscriptionCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}