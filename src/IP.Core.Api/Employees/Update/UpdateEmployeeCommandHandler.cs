using IP.Core.Domain.Employees;

namespace IP.Core.Api.Employees.Update;

internal sealed class UpdateEmployeeCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<UpdateEmployeeResponse>> Handle(
        UpdateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Id);
        UpdateEmployeeRequest request = command.Request;
        UpdateEmployeeUserRequest? user = command.Request.User;
        UpdateEmployeeManagerRequest? manager = command.Request.Manager;

        Employee? currentRecord = await _employeeRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(employee =>
                employee.Id == employeeId,
                cancellationToken);

        if (currentRecord is null) return EmployeeErrors.NotFound;

        currentRecord.SetName(request.Name);
        currentRecord.SetCPF(request.CPF);
        
        if (!currentRecord.CPF.IsValid()) return CPF.Invalid;

        if (user != null) currentRecord.AddUser(user.Id, user.Name);
        else currentRecord.RemoveUser();

        if (manager != null) currentRecord.AddManager(manager.Id, manager.Name);
        else currentRecord.RemoveManager();

        _employeeRepository.Update(currentRecord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateEmployeeResponse(
            currentRecord,
            $"Registro de Colaborador [{currentRecord.Name.Value}] alterado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}