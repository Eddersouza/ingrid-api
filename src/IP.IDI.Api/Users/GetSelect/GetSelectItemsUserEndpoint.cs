namespace IP.IDI.Api.Users.GetSelect;

internal sealed class GetSelectItemsUserEndpoint : IIDIEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Usuarios como Select Items",
                "Lista registros de Usuário como Select Items",
                "Usuários");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsUserQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsUserQuery, GetSelectItemsUserResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsUserResponse> result =
            await commandHandler.Handle(new GetSelectItemsUserQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}