namespace IP.Core.Api.BusinessBranches.GetBusinessBranches;

internal class GetBusinessBranchesEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-branches", Call)
            .RequireAuthorization(BusinessBranchClaim.CanList.Name)
            .MapEndpointProduces<GetBusinessBranchesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Ramos de Negócio",
                "Lista registros de Ramo de Negócio",
                "Ramos de Negócio");
    }
    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetBusinessBranchesQueryRequest request,
       [FromServices] IQueryHandler<GetBusinessBranchesQuery, GetBusinessBranchesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetBusinessBranchesResponse> result =
            await commandHandler.Handle(new GetBusinessBranchesQuery(request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
