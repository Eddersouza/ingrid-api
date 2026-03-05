namespace IP.Core.Api.BusinessBranches.GetSelect;

internal sealed class GetSelectItemsBusinessBranchEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/business-branches/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsBusinessBranchResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Ramos de Negócio como Select Items",
                "Lista registros de Ramos de Negócio como Select Items",
                "Ramos de Negócio");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsBusinessBranchQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsBusinessBranchQuery, GetSelectItemsBusinessBranchResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsBusinessBranchResponse> result =
            await commandHandler.Handle(new GetSelectItemsBusinessBranchQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}