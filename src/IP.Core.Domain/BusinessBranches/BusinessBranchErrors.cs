namespace IP.Core.Domain.BusinessBranches;

public static class BusinessBranchErrors
{
    public static readonly Error BusinessBranchAlreadyExists = Error.Conflict(
       "BusinessBranch.BusinessBranchAlreadyExists",
       "Já existe um registro de Ramo de Negócio com o nome informado.");

    public static readonly Error BusinessBranchNotFound = Error.NotFound(
        "BusinessBranch.BusinessBranchNotFound",
        "Nenhum registro de Ramo de Negócio foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "BusinessBranch.AlreadyActiveStatus",
        $"Registro de Ramo de Negócio já está {(isActive ? "Ativo" : "Desativado")}.");
}
