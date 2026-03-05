namespace IP.AccIPInfo.Api.Transactions.GetCustomerOnSummary;

internal sealed class GetCustomerOnSummaryEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/transactions/summary-accounts/customers/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetCustomerOnSummaryResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Clientes nos Movimentos",
                "Lista registros de Clientes existentes nos Movimentos como Select Items",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetCustomerOnSummaryQueryRequest request,
       [FromServices] IQueryHandler<GetCustomerOnSummaryQuery, GetCustomerOnSummaryResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetCustomerOnSummaryResponse> result =
            await commandHandler.Handle(new GetCustomerOnSummaryQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}