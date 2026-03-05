namespace IP.AccIPInfo.Domain.AccountSubscriptions;

public class AccountSubscriptionErrors
{
    public static readonly Error AccountSubscriptionAlreadyExists = Error.Conflict(
        "AccountSubscription.AccountSubscriptionAlreadyExists",
        "Já existe um registro de Plano com o nome informado.");

    public static readonly Error AccountSubscriptionNotFound = Error.NotFound(
        "AccountSubscription.AccountSubscriptionNotFound",
        "Nenhum registro de Plano foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "AccountSubscription.AlreadyActiveStatus",
        $"Registro de Plano já está {(isActive ? "Ativo" : "Desativado")}.");
}