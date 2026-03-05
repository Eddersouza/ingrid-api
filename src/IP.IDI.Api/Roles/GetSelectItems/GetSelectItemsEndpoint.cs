namespace IP.IDI.Api.Roles.GetSelectItems;

internal sealed class GetSelectItemsEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsRolesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Perfil como Select Items",
                "Lista registros de Perfil como Select Items",
                "Perfis");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsRolesQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsRolesQuery, GetSelectItemsRolesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsRolesResponse> result =
            await commandHandler.Handle(new GetSelectItemsRolesQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}