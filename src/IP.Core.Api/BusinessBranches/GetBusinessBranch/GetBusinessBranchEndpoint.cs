namespace IP.Core.Api.BusinessBranches.GetBusinessBranch;

internal class GetBusinessBranchEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-branches/{id:guid}", Call)
            .RequireAuthorization(BusinessBranchClaim.CanRead.Name)
            .MapEndpointProduces<GetBusinessBranchResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Ramo de Negócio",
                "Busca registro de Ramo de Negócio",
                "Ramos de Negócio");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetBusinessBranchQuery, GetBusinessBranchResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetBusinessBranchResponse> result =
            await commandHandler.Handle(new GetBusinessBranchQuery(id), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
