namespace IP.Core.Api.Employees.Create;

internal class CreateEmployeeEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/employees", Call)
            .RequireAuthorization(EmployeeClaim.CanCreate.Name)
            .MapEndpointProduces<CreateEmployeeResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Colaborador",
                "Cria novo registro de Colaborador.",
                "Colaboradores");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateEmployeeRequest request,
        [FromServices] ICommandHandler<CreateEmployeeCommand, CreateEmployeeResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateEmployeeResponse> result =
            await commandHandler.Handle(new CreateEmployeeCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}