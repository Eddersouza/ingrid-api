namespace IP.Shared.Abstractions.Errors;

public record Error
{
    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    public string Code { get; }

    public string Description { get; }

    public ErrorType Type { get; }
    public string TypeDescription => Type.GetDescription();

    public static readonly Error None =
        new(string.Empty, string.Empty, ErrorType.Failure);

    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    public static Error Problem(string code, string description) =>
       new(code, description, ErrorType.Problem);

    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static Error Forbidden() =>
        new("Forbidden", "É necessário ter permissão para acessar esse recurso.", ErrorType.Forbidden);

    public static Error UnprocessableEntity() =>
        new("Unprocessable.Entity",
            "Ocorreu um erro na leitura do request.",
            ErrorType.UnprocessableEntity);

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public enum ErrorType
{
    [Description("Falha")] Failure = 0,
    [Description("Validacao")] Validation = 1,
    [Description("Nao Encontrado")] NotFound = 2,
    [Description("Conflito")] Conflict = 3,
    [Description("Erro leitura de objeto")] UnprocessableEntity = 4,
    [Description("Erro geral")] Problem = 5,
    [Description("Proibido")] Forbidden = 6,
}