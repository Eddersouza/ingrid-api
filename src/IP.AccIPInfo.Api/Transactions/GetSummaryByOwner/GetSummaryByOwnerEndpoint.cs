namespace IP.AccIPInfo.Api.Transactions.GetSummaryByOwner;

internal sealed class GetSummaryByOwnerEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/accounts/transactions/summary/owner/{ownerId:guid}", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSummaryByOwnerResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Transações Totalizadas Por Responsável",
                "Total de transações por responsável.",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid ownerId,
        [AsParameters] GetSummaryByOwnerRequest request,
        [FromServices] IQueryHandler<GetSummaryByOwnerQuery, GetSummaryByOwnerResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetSummaryByOwnerResponse> result =
            await commandHandler.Handle(new GetSummaryByOwnerQuery(ownerId, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}