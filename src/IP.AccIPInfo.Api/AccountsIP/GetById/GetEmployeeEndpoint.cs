namespace IP.AccIPInfo.Api.AccountsIP.GetById;

internal class GetAccountIPEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/{id:guid}", Call)
            .RequireAuthorization(AccountIPClaim.CanRead.Name)
            .MapEndpointProduces<GetAccountIPResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Contas",
                "Busca registro de Contas",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetAccountIPQuery, GetAccountIPResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetAccountIPResponse> result =
            await commandHandler.Handle(new GetAccountIPQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}