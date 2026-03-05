namespace IP.Core.Api.Employees.Create;

internal sealed class CreateEmployeeCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<CreateEmployeeCommand, CreateEmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<CreateEmployeeResponse>> Handle(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        Employee employee = Employee.Create(command.Request.Name, command.Request.CPF);

        if (!employee.CPF.IsValid()) return CPF.Invalid;

        CreateEmployeeUserRequest? user = command.Request.User;
        if (user != null) employee.AddUser(user.Id, user.Name);

        CreateEmployeeManagerRequest? manager = command.Request.Manager;
        if (manager != null) employee.AddManager(manager.Id, manager.Name);

        await _employeeRepository.Create(employee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateEmployeeResponse(
            employee,
            $"Registro de Colaborador [{employee.Name.Value}] criado com sucesso!");

        return Result.Success(response);
    }
}