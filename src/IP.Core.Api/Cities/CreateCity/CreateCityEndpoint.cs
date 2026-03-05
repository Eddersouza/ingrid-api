namespace IP.Core.Api.Cities.CreateCity;

internal class CreateCityEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/cities", Call)
            .RequireAuthorization(CityClaim.CanCreate.Name)
            .MapEndpointProduces<CreateCityResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Cidade",
                "Cria novo registro de Cidade",
                "Cidades");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateCityRequest request,
        [FromServices] ICommandHandler<CreateCityCommand, CreateCityResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateCityResponse> result =
            await commandHandler.Handle(new CreateCityCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
