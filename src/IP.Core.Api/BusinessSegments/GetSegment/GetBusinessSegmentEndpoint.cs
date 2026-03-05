namespace IP.Core.Api.BusinessSegments.GetSegment;

internal sealed class GetBusinessSegmentEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-branches/{businessBranchId}/business-segments/{id:guid}", Call)
            .RequireAuthorization(BusinessSegmentClaim.CanRead.Name)
            .MapEndpointProduces<GetBusinessSegmentResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Segmentos de Negócio",
                "Busca Registro de Segmentos",
                "Segmentos de Negócio");
    }
    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromRoute] Guid businessBranchId,
        [FromServices] IQueryHandler<GetBusinessSegmentQuery, GetBusinessSegmentResponse> queryHandler,
        CancellationToken cancellationToken)
    {
        Result<GetBusinessSegmentResponse> result =
            await queryHandler.Handle(new GetBusinessSegmentQuery(id), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
