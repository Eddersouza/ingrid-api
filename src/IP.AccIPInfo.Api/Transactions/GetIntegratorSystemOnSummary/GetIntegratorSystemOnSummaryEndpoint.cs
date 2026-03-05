namespace IP.AccIPInfo.Api.Transactions.GetIntegratorSystemOnSummary;

internal sealed class GetIntegratorSystemOnSummaryEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/transactions/summary-accounts/integrators/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetIntegratorSystemOnSummaryResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Revendas nos Movimentos",
                "Lista registros de Revendas existentes nos Movimentos como Select Items",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetIntegratorSystemOnSummaryQueryRequest request,
       [FromServices] IQueryHandler<GetIntegratorSystemOnSummaryQuery, GetIntegratorSystemOnSummaryResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetIntegratorSystemOnSummaryResponse> result =
            await commandHandler.Handle(new GetIntegratorSystemOnSummaryQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}