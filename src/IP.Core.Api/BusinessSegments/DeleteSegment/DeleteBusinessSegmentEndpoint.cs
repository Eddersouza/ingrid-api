namespace IP.Core.Api.BusinessSegments.DeleteSegment;

internal class DeleteBusinessSegmentEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/business-branches/{businessBranchId}/business-segments/{id:guid}", Call)
            .RequireAuthorization(BusinessSegmentClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteBusinessSegmentResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Segmento de Negócio",
                "Remover registro de Segmento de Negócio",
                "Segmentos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromRoute] Guid businessBranchId,
        [FromBody] DeleteBusinessSegmentRequest request,
        [FromServices] ICommandHandler<DeleteBusinessSegmentCommand, DeleteBusinessSegmentResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteBusinessSegmentResponse> result =
            await commandHandler.Handle(new DeleteBusinessSegmentCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
