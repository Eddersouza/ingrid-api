namespace IP.Core.Api.BusinessBranches.Create;

internal class CreateBusinessBranchEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/business-branches", Call)
            .RequireAuthorization(BusinessBranchClaim.CanCreate.Name)
            .MapEndpointProduces<CreateBusinessBranchResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Ramo de Negócio",
                "Cria novo registro de Ramo de Negócio",
                "Ramos de Negócio");
    }
    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateBusinessBranchRequest request,
        [FromServices] ICommandHandler<CreateBusinessBranchCommand, CreateBusinessBranchResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateBusinessBranchResponse> result =
            await commandHandler.Handle(new CreateBusinessBranchCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
