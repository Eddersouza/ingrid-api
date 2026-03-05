namespace IP.AccIPInfo.Api.Transactions.SummaryAccountsByOwner;

internal sealed class GetSummaryAccountsByOwnerEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/transactions/summary-accounts/owner/{ownerId:guid}", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSummaryAccountsByOwnerResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Contas Totalizadas Por Responsável",
                "Lista registros de Contas com totalização.",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid ownerId,
        [AsParameters] GetSummaryAccountsByOwnerRequest request,
        [FromServices] IQueryHandler<GetSummaryAccountsByOwnerQuery, GetSummaryAccountsByOwnerResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetSummaryAccountsByOwnerResponse> result =
            await commandHandler.Handle(new GetSummaryAccountsByOwnerQuery(ownerId, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}