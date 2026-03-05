namespace IP.Core.Api.BusinessSegments.GetSegments;

internal class GetBusinessSegmentsEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("business-branches/{businessBranchId}/business-segments", Call)
            .RequireAuthorization(BusinessSegmentClaim.CanList.Name)
            .MapEndpointProduces<GetBusinessSegmentsResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Segmentos de Negócio",
                "Lista Registros de Segmentos de Negócio",
                "Segmentos de Negócio");
    }
    private static async Task<IResult> Call(HttpContext context,
        [AsParameters] GetBusinessSegmentsQueryRequest request,
        [FromRoute] Guid businessBranchId, 
        [FromServices] IQueryHandler<GetBusinessSegmentsQuery, GetBusinessSegmentsResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        request.BusinessBranchId = businessBranchId;
        Result<GetBusinessSegmentsResponse> result =
            await commandHandler.Handle(new GetBusinessSegmentsQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
