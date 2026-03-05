namespace IP.IDI.Api.Roles.CreateRole;

internal sealed class CreateRoleEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/roles", Call)
            .RequireAuthorization(RoleClaim.CanCreate.Name)
            .MapEndpointProduces<CreateRoleResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Perfil",
                "Cria novo registro de Perfil",
                "Perfis");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateRoleRequest request,
        [FromServices] ICommandHandler<CreateRoleCommand, CreateRoleResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateRoleResponse> result =
            await commandHandler.Handle(new CreateRoleCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}