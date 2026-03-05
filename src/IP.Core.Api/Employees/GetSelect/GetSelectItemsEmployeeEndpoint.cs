namespace IP.Core.Api.Employees.GetSelect;

internal sealed class GetSelectItemsEmployeeEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/employees/select-items", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetSelectItemsEmployeeResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Colaboradores como Select Items",
                "Lista registros de Colaboradores como Select Items",
                "Colaboradores");
    }

    private static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetSelectItemsEmployeeQueryRequest request,
       [FromServices] IQueryHandler<GetSelectItemsEmployeeQuery, GetSelectItemsEmployeeResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetSelectItemsEmployeeResponse> result =
            await commandHandler.Handle(new GetSelectItemsEmployeeQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}