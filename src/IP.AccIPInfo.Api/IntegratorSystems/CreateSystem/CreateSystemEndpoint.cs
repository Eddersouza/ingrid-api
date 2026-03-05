namespace IP.AccIPInfo.Api.IntegratorSystems.CreateSystem;

internal class CreateSystemEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/integrator-systems", Call)
            .RequireAuthorization(IntegratorSystemClaim.CanCreate.Name)
            .MapEndpointProduces<CreateSystemResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Sistema Integrador",
                "Cria novo registro de Sistema Integrador",
                "Sistemas");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateSystemRequest request,
        [FromServices] ICommandHandler<CreateSystemCommand, CreateSystemResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateSystemResponse> result =
            await commandHandler.Handle(new CreateSystemCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}