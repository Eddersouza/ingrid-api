namespace IP.Core.Api.Employees.Get;

internal class GetEmployeesEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/employees", Call)
            .RequireAuthorization(EmployeeClaim.CanList.Name)
            .MapEndpointProduces<GetEmployeesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Colaboradores",
                "Lista registros de Colaboradores",
                "Colaboradores");
    }

    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetEmployeesRequest request,
       [FromServices] IQueryHandler<GetEmployeesQuery, GetEmployeesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetEmployeesResponse> result =
            await commandHandler.Handle(new GetEmployeesQuery(request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}