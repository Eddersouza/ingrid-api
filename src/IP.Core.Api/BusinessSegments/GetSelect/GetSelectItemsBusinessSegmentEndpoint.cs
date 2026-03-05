namespace IP.Core.Api.BusinessSegments.GetSelect;

internal sealed class GetSelectItemsBusinessSegmentEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-branches/segments/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsBusinessSegmentResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Segmentos de Negócio como Select Items",
                "Lista registros de Segmentos de Negócio como Select Items",
                "Segmentos de Negócio");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsBusinessSegmentQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsBusinessSegmentQuery, GetSelectItemsBusinessSegmentResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsBusinessSegmentResponse> result =
            await commandHandler.Handle(new GetSelectItemsBusinessSegmentQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}