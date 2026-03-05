namespace IP.Core.Domain.Employees;

public static class EmployeeErrors
{
    public static readonly Error AlreadyExists = Error.Conflict(
        "Employee.AlreadyExists",
        "Já existe um registro de Colaborador com o nome informado.");

    public static readonly Error NotFound = Error.NotFound(
        "Employee.NotFound",
        "Nenhum registro de Colaborador foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "Employee.AlreadyActiveStatus",
        $"Registro de Colaborador já está {(isActive ? "Ativo" : "Desativado")}.");
}