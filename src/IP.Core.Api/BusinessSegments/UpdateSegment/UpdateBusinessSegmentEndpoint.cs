namespace IP.Core.Api.BusinessSegments.UpdateSegment;

internal class UpdateBusinessSegmentEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/business-branches/{businessBranchId}/business-segments/{id:guid}", Call)
            .RequireAuthorization(BusinessSegmentClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateBusinessSegmentResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Segmento de Negócio",
                "Altera registro de Segmento de Negócio",
                "Segmentos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromRoute] Guid businessBranchId,
        [FromBody] UpdateBusinessSegmentRequest request,
        [FromServices] ICommandHandler<UpdateBusinessSegmentCommand, UpdateBusinessSegmentResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateBusinessSegmentResponse> result =
            await commandHandler.Handle(new UpdateBusinessSegmentCommand(id, request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
