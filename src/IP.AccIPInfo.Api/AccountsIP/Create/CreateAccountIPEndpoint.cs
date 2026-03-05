namespace IP.AccIPInfo.Api.AccountsIP.Create;

internal sealed class CreateAccountIPEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts", Call)
            .RequireAuthorization(AccountIPClaim.CanCreate.Name)
            .MapEndpointProduces<CreateAccountIPResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Contas",
                "Cria novo registro de Contas.",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateAccountIPRequest request,
        [FromServices] ICommandHandler<CreateAccountIPCommand, CreateAccountIPResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateAccountIPResponse> result =
            await commandHandler.Handle(new CreateAccountIPCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}