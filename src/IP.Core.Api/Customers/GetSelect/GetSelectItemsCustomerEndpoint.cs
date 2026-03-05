namespace IP.Core.Api.Customers.GetSelect;

internal sealed class GetSelectItemsCustomerEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/customers/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsCustomerResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Clientes como Select Items",
                "Lista registros de Clientes como Select Items",
                "Clientes");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsCustomerQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsCustomerQuery, GetSelectItemsCustomerResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsCustomerResponse> result =
            await commandHandler.Handle(new GetSelectItemsCustomerQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}