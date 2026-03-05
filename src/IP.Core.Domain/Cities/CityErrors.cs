namespace IP.Core.Domain.Cities;

public static class CityErrors
{
    public static readonly Error CityAlreadyExists = Error.Conflict(
        "City.CityAlreadyExists",
        "Já existe um registro de Cidade com o nome informado.");

    public static readonly Error CityNotFound = Error.NotFound(
        "City.CityNotFound",
        "Nenhum registro de Cidade foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "City.AlreadyActiveStatus",
        $"Registro de Cidade já está {(isActive ? "Ativo" : "Desativado")}.");
}
