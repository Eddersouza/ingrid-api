namespace IP.AccIPInfo.Domain.AccountsIP;

public static class AccountIPErrors
{
    public static readonly Error AlreadyExists = Error.Conflict(
        "Account.AlreadyExists",
        "Já existe um registro de Conta com o número informado.");

    public static readonly Error NotFound = Error.NotFound(
        "Account.NotFound",
        "Nenhum registro de Conta foi encontrado.");
}
