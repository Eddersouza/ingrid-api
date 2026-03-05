using IP.Shared.Abstractions;

namespace IP.IDI.Domain.Users;

public static class UserErrors
{
    public static readonly Error AlreadyHasThisProfile = Error.Conflict(
        "User.AlreadyHasThisProfile",
        "Usuário já possui esse Perfil.");

    public static readonly Error AuthenticateInfoWrong = Error.Validation(
        "User.AuthenticateInfoWrong",
        "Verifique o Usuário/Email e Senha e tente novamente.");

    public static readonly Error ChangePasswordInvalidToken = Error.Problem(
        "User.ConfirmUserInvalidToken",
        "Não foi possivel trocar sua senha, entre em contato com o suporte.");

    public static readonly Error ConfirmUserInvalidToken = Error.Problem(
            "User.ConfirmUserInvalidToken",
        "Não foi possivel confirmar seu Usuário, entre em contato com o suporte.");

    public static readonly Error EmailNotConfirmed = Error.Validation(
        "User.EmailNotConfirmed",
        "Email ainda nao confirmado. Verifique sua caixa de email.");

    public static readonly Error ForbiddenAccess = Error.Forbidden();

    public static readonly Error NotFound = Error.NotFound(
                "User.NotFound",
        "Usuário não encontrado.");

    public static readonly Error PasswordGreaterThanMaxLength = Error.Validation(
        "User.PasswordGreaterThanMaxLength",
        ValidationMessage.RequiredMaxLengthField("Senha", AppUser.PASSWORD_MAX_LENGTH));

    public static readonly Error PasswordLessThanMinLength = Error.Validation(
       "User.PasswordLessThanMinLength",
       ValidationMessage.RequiredMinLengthField("Senha", AppUser.PASSWORD_MIN_LENGTH));

    public static readonly Error PasswordMustHaveDigits = Error.Validation(
                "User.PasswordMustHaveDigits",
        "Campo [Senha] precisa de números.");

    public static readonly Error PasswordMustHaveLowercase = Error.Validation(
        "User.PasswordMustHaveSpecialChars",
        "Campo [Senha] precisa de letras minusculas.");

    public static readonly Error PasswordMustHaveSpecialChars = Error.Validation(
        "User.PasswordMustHaveSpecialChars",
        "Campo [Senha] precisa de caracteres especiais.");

    public static readonly Error PasswordMustHaveUppercase = Error.Validation(
        "User.PasswordMustHaveUppercase",
        "Campo [Senha] precisa de letras maiusculas.");

    public static readonly Error RoleNotFound = Error.Problem(
        "User.RoleNotFound",
        "Usuário esta sem Perfil associado.");

    public static Error UserLocked(int attempts, int minutesToLockoutEnd) => Error.Conflict(
        "User.UserLocked",
        $"Usuário bloqueado por excesso de tentativas. Maximo de {attempts} tentativas. Aguarde {minutesToLockoutEnd} minutos e tente novamente.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "User.AlreadyActiveStatus",
        $"Registro de Usuário ja está {(isActive ? "Ativo" : "Desativado")}.");
}