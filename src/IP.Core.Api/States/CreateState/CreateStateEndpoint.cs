namespace IP.Core.Api.States.CreateState;

internal class CreateStateEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/states", Call)
            .RequireAuthorization(StateClaim.CanCreate.Name)
            .MapEndpointProduces<CreateStateResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Estado",
                "Cria novo registro de Estado",
                "Estados");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateStateRequest request,
        [FromServices] ICommandHandler<CreateStateCommand, CreateStateResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateStateResponse> result =
            await commandHandler.Handle(new CreateStateCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
