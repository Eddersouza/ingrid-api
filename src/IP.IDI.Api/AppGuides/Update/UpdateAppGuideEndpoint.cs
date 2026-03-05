namespace IP.IDI.Api.AppGuides.Update;

internal sealed class UpdateAppGuideEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/app-guides/{id:guid}", Call)
            .RequireAuthorization(AppGuideClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateAppGuideResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Guia do Sistema",
                "Altera registro de Guia do Sistema",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateAppGuideRequest request,
        [FromServices] ICommandHandler<UpdateAppGuideCommand, UpdateAppGuideResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateAppGuideResponse> result =
            await commandHandler.Handle(new UpdateAppGuideCommand(id, request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
