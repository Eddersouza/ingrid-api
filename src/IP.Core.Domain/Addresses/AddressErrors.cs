namespace IP.Core.Domain.Addresses;

public static class AddressErrors
{
    public static readonly Error AddressNotFound = Error.NotFound(
        "Address.AddressNotFound",
        "Nenhum registro de Endereço foi encontrado.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "Address.AlreadyActiveStatus",
        $"Registro de Endereço já está {(isActive ? "Ativo" : "Desativado")}.");

    public static readonly Error CodeEmpty = Error.Validation(
        "Address.CodeEmpty",
        "Código CEP do Endereço deve ser informado.");

    public static readonly Error CityIdEmpty = Error.Validation(
        "Address.CityIdEmpty",
        "Cidade do Endereço deve ser selecionada.");

    public static readonly Error AddressAlreadyExists = Error.Conflict(
        "Address.AddressAlreadyExists",
        $"Registro de Endereço já existe");
}
