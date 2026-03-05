namespace IP.Core.Api.Customers.Get;

internal class GetCustomersEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/customers", Call)
            .RequireAuthorization(CustomerClaim.CanList.Name)
            .MapEndpointProduces<GetCustomersResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Clientes",
                "Lista registros de Clientes",
                "Clientes");
    }

    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetCustomersRequest request,
       [FromServices] IQueryHandler<GetCustomersQuery, GetCustomersResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetCustomersResponse> result =
            await commandHandler.Handle(new GetCustomersQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}