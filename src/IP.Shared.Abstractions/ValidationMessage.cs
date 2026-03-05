namespace IP.Shared.Abstractions;

public static class ValidationMessage
{
    public static string FieldAMustBeEqualFieldB(string labelA, string labelB) =>
       $"Campo [{labelA}] deve ser igual [{labelB}]!";

    public static string RequiredEmailField(string label) =>
             $"Campo [{label}] deve ter formato de e-mail!";

    public static string RequiredField(string label) =>
        $"Campo [{label}] é obrigatorio!";

    public static string RequiredMaxLengthField(string label, int maxLength) =>
        $"Campo [{label}] deve ser menor que [{maxLength}] caracteres!";

    public static string RequiredMinLengthField(string label, int minLength) =>
        $"Campo [{label}] deve ser maior que [{minLength}] caracteres!";
}