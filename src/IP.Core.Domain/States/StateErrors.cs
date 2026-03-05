namespace IP.Core.Domain.States;

public static class StateErrors
{
    public static readonly Error StateAlreadyExists = Error.Conflict(
        "State.StateAlreadyExists",
        "Já existe um registro de Estado com o nome informado.");

    public static readonly Error StateNotFound = Error.NotFound(
        "State.StateNotFound",
        "Nenhum registro de Estado foi encontrado.");
}
