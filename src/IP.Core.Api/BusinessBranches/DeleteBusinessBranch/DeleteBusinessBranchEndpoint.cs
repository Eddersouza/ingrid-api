namespace IP.Core.Api.BusinessBranches.DeleteBusinessBranch;

internal class DeleteBusinessBranchEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/business-branches/{id:guid}", Call)
            .RequireAuthorization(BusinessBranchClaim.CanUpdate.Name)
            .MapEndpointProduces<DeleteBusinessBranchResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Ramo de Negócio",
                "Remover registro de Ramo de Negócio",
                "Ramos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteBusinessBranchRequest request,
        [FromServices] ICommandHandler<DeleteBusinessBranchCommand, DeleteBusinessBranchResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteBusinessBranchResponse> result =
            await commandHandler.Handle(new DeleteBusinessBranchCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }

}
