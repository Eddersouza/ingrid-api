namespace IP.Core.Api.Employees.GetById;

internal class GetEmployeeEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/employees/{id:guid}", Call)
            .RequireAuthorization(EmployeeClaim.CanRead.Name)
            .MapEndpointProduces<GetEmployeeResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Colaborador",
                "Busca registro de Colaborador",
                "Colaboradores");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetEmployeeQuery, GetEmployeeResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetEmployeeResponse> result =
            await commandHandler.Handle(new GetEmployeeQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}