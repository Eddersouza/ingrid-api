namespace IP.AccIPInfo.Api.AccountsIP.Get;

internal sealed class GetAccountIPEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts", Call)
            .RequireAuthorization(AccountIPClaim.CanList.Name)
            .MapEndpointProduces<GetAccountsIPResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Contas",
                "Lista registros de Contas",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
           [AsParameters] GetAccountsIPRequest request,
       [FromServices] IQueryHandler<GetAccountsIPQuery, GetAccountsIPResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetAccountsIPResponse> result =
            await commandHandler.Handle(new GetAccountsIPQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}