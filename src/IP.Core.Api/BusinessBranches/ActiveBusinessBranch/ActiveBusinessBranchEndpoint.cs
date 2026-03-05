namespace IP.Core.Api.BusinessBranches.ActiveBusinessBranch;

internal class ActiveBusinessBranchEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/business-branches/active/{id:guid}", Call)
            .RequireAuthorization(BusinessBranchClaim.CanUpdate.Name)
            .MapEndpointProduces<ActiveBusinessBranchResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativar/Desativar Ramo de Negócio",
                "Ativa/Desativar registro de Ramo de Negócio",
                "Ramos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ActiveBusinessBranchRequest request,
        [FromServices] ICommandHandler<ActiveBusinessBranchCommand, ActiveBusinessBranchResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveBusinessBranchResponse> result =
            await commandHandler.Handle(new ActiveBusinessBranchCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
