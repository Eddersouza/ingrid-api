namespace IP.IDI.Domain.AppGuides;

public static class AppGuideErrors
{
    public static readonly Error AlreadyExists = Error.Conflict(
        "AppGuide.AlreadyExists",
        "Já existe um registro de Guia do Sistema com o nome ou link id informado.");

    public static readonly Error NotFound = Error.NotFound(
        "AppGuide.NotFound",
        "Registro de Guia do Sistema não encontrado.");

    public static readonly Error InvalidLinkIdPattern = Error.Problem(
        "AppGuide.InvalidLinkIdPattern",
        "Link de Guia do Sistema inválido. Use apenas caracteres alfanuméricos e underline ou hifen.");

    public static readonly Error UserNotViewed  = Error.Conflict(
        "AppGuide.UserNotViewed",
        "Usuário ainda não visualizou o Guia do Sistema.");

    public static Error AlreadyActiveStatus(bool isActive) => Error.Conflict(
        "AppGuide.AlreadyActiveStatus",
        $"Registro de Guia de Aplicativo ja está {(isActive ? "Ativo" : "Desativado")}.");
}