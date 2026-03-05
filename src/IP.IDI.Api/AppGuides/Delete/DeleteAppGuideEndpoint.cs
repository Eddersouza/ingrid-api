namespace IP.IDI.Api.AppGuides.Delete;

internal class DeleteAppGuideEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/app-guides/{id:guid}", Call)
            .RequireAuthorization(AppGuideClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteAppGuideResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Guia do Sistema",
                "Remover registro de Guia do Sistema",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,        
        [FromServices] ICommandHandler<DeleteAppGuideCommand, DeleteAppGuideResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteAppGuideResponse> result =
            await commandHandler.Handle(new DeleteAppGuideCommand(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}