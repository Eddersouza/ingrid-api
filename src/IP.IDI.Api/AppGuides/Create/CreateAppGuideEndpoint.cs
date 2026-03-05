namespace IP.IDI.Api.AppGuides.Create;

internal sealed class CreateAppGuideEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/app-guides", Call)
            .RequireAuthorization(AppGuideClaim.CanCreate.Name)
            .MapEndpointProduces<CreateAppGuideResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Guia do Sistema",
                "Cria registro de Guia do Sistema",
                "Guia do Sistema");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateAppGuideRequest request,
        [FromServices] ICommandHandler<CreateAppGuideCommand, CreateAppGuideResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateAppGuideResponse> result =
            await commandHandler.Handle(new CreateAppGuideCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
