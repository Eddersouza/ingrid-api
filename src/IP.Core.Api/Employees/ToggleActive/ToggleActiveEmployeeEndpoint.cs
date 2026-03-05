namespace IP.Core.Api.Employees.ToggleActive;

internal sealed class ToggleActiveEmployeeEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/employees/active/{id:guid}", Call)
            .RequireAuthorization(EmployeeClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ToggleActiveEmployeeResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativa/Desativa Colaborador",
                "Ativa ou Desativa registro de Colaborador",
                "Colaboradores");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ToggleActiveEmployeeRequest request,
        [FromServices] ICommandHandler<ToggleActiveEmployeeCommand, ToggleActiveEmployeeResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ToggleActiveEmployeeResponse> result =
            await commandHandler.Handle(new ToggleActiveEmployeeCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}