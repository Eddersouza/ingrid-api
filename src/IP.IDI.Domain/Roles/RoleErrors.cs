namespace IP.IDI.Domain.Roles;

public static class RoleErrors
{
    public static readonly Error RoleAlreadyExists = Error.Conflict(
        "Role.RoleAlreadyExists",
        "Já existe um registro de Perfil com o nome informado.");

    public static readonly Error NotFound = Error.NotFound(
        "Role.NotFound",
        "Registro de Perfil não encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "Role.AlreadyActiveStatus",
        $"Registro de Perfil já está {(isActive ? "Ativo" : "Desativado")}.");
}