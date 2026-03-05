namespace IP.Core.Domain.Customers;

public static class CustomerErrors
{
    public static readonly Error AlreadyExists = Error.Conflict(
        "Customer.AlreadyExists",
        "Já existe um registro de Cliente com o nome informado.");

    public static readonly Error NotFound = Error.NotFound(
        "Customer.NotFound",
        "Nenhum registro de Cliente foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "Customer.AlreadyActiveStatus",
        $"Registro de Cliente já está {(isActive ? "Ativo" : "Desativado")}.");
}
