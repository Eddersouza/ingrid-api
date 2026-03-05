namespace IP.Core.Api.BusinessSegments.ActiveSegment;

internal class ActiveBusinessSegmentEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/business-branches/{businessBranchId}/business-segments/active/{id:guid}", Call)
            .RequireAuthorization(BusinessSegmentClaim.CanUpdate.Name)
            .MapEndpointProduces<ActiveBusinessSegmentResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativar/Desativar Segmento de Negócio",
                "Ativa/Desativar registro de Segmento de Negócio",
                "Segmentos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromRoute] Guid businessBranchId,
        [FromBody] ActiveBusinessSegmentRequest request,
        [FromServices] ICommandHandler<ActiveBusinessSegmentCommand, ActiveBusinessSegmentResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveBusinessSegmentResponse> result =
            await commandHandler.Handle(new ActiveBusinessSegmentCommand(id, request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
