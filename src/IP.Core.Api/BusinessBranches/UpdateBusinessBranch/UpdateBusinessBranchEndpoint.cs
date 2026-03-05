namespace IP.Core.Api.BusinessBranches.UpdateBusinessBranch;

internal class UpdateBusinessBranchEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/business-branches/{id:guid}", Call)
            .RequireAuthorization(BusinessBranchClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateBusinessBranchResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Ramo de Negócio",
                "Altera registro de Ramo de Negócio",
                "Ramos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateBusinessBranchRequest request,
        [FromServices] ICommandHandler<UpdateBusinessBranchCommand, UpdateBusinessBranchResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateBusinessBranchResponse> result =
            await commandHandler.Handle(new UpdateBusinessBranchCommand(id, request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
