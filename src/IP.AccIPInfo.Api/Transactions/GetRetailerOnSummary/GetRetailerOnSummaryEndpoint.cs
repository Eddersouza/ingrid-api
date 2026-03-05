namespace IP.AccIPInfo.Api.Transactions.GetRetailerOnSummary;

internal sealed class GetRetailerOnSummaryEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/transactions/summary-accounts/retailers/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetRetailerOnSummaryResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Revendas nos Movimentos",
                "Lista registros de Revendas existentes nos Movimentos como Select Items",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetRetailerOnSummaryQueryRequest request,
       [FromServices] IQueryHandler<GetRetailerOnSummaryQuery, GetRetailerOnSummaryResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetRetailerOnSummaryResponse> result =
            await commandHandler.Handle(new GetRetailerOnSummaryQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}