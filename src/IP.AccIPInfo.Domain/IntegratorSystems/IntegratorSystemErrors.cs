namespace IP.AccIPInfo.Domain.IntegratorSystems;

public static class IntegratorSystemErrors
{
    public static readonly Error IntegratorSystemAlreadyExists = Error.Conflict(
        "IntegratorSystem.IntegratorSystemAlreadyExists",
        "Já existe um registro de Sistema com o nome informado.");

    public static readonly Error IntegratorSystemNotFound = Error.NotFound(
        "IntegratorSystem.IntegratorSystemNotFound",
        "Nenhum registro de Sistema foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "IntegratorSystem.AlreadyActiveStatus",
        $"Registro de Sistema já está {(isActive ? "Ativo" : "Desativado")}.");
}