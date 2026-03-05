namespace IP.AccIPInfo.Api.Transactions.GetOwnerOnSummary;

internal sealed class GetOwnerOnSummaryEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/transactions/summary-accounts/owners/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetOwnerOnSummaryResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Responsáveis nos Movimentos",
                "Lista registros de Responsáveis existentes nos Movimentos como Select Items",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetOwnerOnSummaryQueryRequest request,
       [FromServices] IQueryHandler<GetOwnerOnSummaryQuery, GetOwnerOnSummaryResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetOwnerOnSummaryResponse> result =
            await commandHandler.Handle(new GetOwnerOnSummaryQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}