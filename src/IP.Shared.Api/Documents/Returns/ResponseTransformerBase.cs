using Microsoft.OpenApi.Any;

namespace IP.Shared.Api.Documents.Returns;

internal class ResponseTransformerBase(int statusCode)
{
    public int StatusCode { get; init; } = statusCode;
    private const string StringType = "string";
    protected Dictionary<int, string> StatusCodesDescription { get; init; } = new Dictionary<int, string> {
        {200, "[OK]: Requisição realizada com sucesso."},
        {201, "[Created]: Recurso criado com sucesso."},
        {202, "[Accepted]: Requisição aceita para processamento."},
        {400, "[Bad Request]: A requisição está inválida ou possui parâmetros incorretos."},
        {401, "[Unauthorized]: Credenciais inválidas ou não fornecidas."},
        {403, "[Forbidden]: Vocé não tem permissão para acessar este recurso."},
        {404, "[Not Found]: Recurso não encontrado."},
        {409, "[Conflict]: Conflito ao processar a requisição. O recurso ja existe ou há dados conflitantes."},
        {422, "[Unprocessable Entity]: Os dados fornecidos são inválidos ou não puderam ser processados."},
        {500, "[Internal Server Error]: Ocorreu um erro interno no servidor."}
    };

    protected Dictionary<int, string> StatusCodesDetails { get; init; } = new Dictionary<int, string> {
        {200, ""},
        {201, ""},
        {202, ""},
        {400, "Um ou mais parâmetros estão incorretos."},
        {401, "Credenciais inválidas ou não fornecidas."},
        {403, "Você não possui permissão para acessar este recurso."},
        {404, "O recurso solicitado não foi encontrado."},
        {409, "Não foi possível processar a requisição devido a conflito de dados."},
        {422, "Os dados enviados não estão no formato esperado."},
        {500, "Ocorreu um erro inesperado ao processar a requisição."},
    };

    protected Dictionary<int, string> StatusCodesURI { get; init; } = new Dictionary<int, string> {
        {200, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1"},
        {201, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.2"},
        {202, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.3"},
        {400, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"},
        {401, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1"},
        {403, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3"},
        {404, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"},
        {409, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8"},
        {422, "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2"},
        {500, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"}
    };
    protected Dictionary<int, string> StatusCodesTitle { get; init; } = new Dictionary<int, string> {
        {200, ""},
        {201, ""},
        {202, ""},
        {400, "BadRequest"},
        {401, "Unauthorized"},
        {403, "Forbidden"},
        {404, "NotFound"},
        {409, "Conflict"},
        {422, "UnprocessableEntity"},
        {500, "InternalServerError"},
    };

    public bool IsCurrentStatusCode(OpenApiOperationTransformerContext context) =>
        context.Description.SupportedResponseTypes
        .Any(r => r.StatusCode == StatusCode);

    protected OpenApiResponse CreateSchemaResponse()
    {
        OpenApiSchema problemDetailsSchema = CreateProblemDetail();

        return new OpenApiResponse
        {
            Description = StatusCodesDescription[StatusCode],
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/problem+json"] = new OpenApiMediaType
                {
                    Schema = problemDetailsSchema
                }
            }
        };
    }

    private static OpenApiSchema CreateErrorsSchema()
    {
        return new OpenApiSchema
        {
            Type = "array|optional",
            Description = "Lista de erros de validação detalhados",
            Items = new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["code"] = new OpenApiSchema
                    {
                        Type = StringType,
                        Description = "Código do erro",
                        Example = new OpenApiString("MinimumLengthValidator")
                    },
                    ["description"] = new OpenApiSchema
                    {
                        Type = StringType,
                        Description = "Descrição detalhada do erro",
                        Example = new OpenApiString("Campo [Name] deve ser maior que [10] caracteres!")
                    }
                },
            },
            Example = new OpenApiArray
                    {
                        new OpenApiObject
                        {
                            ["code"] = new OpenApiString("MaximumLengthValidator"),
                            ["description"] = new OpenApiString("Campo [Name] deve ser maior que [10] caracteres!")
                        },
                        new OpenApiObject
                        {
                            ["code"] = new OpenApiString("MaximumLengthValidator"),
                            ["description"] = new OpenApiString("Campo [Documento] deve ser menor que [100] caracteres!")
                        }
                    }
        };
    }

    private OpenApiSchema CreateProblemDetail()
    {
        Dictionary<string, OpenApiSchema> fields = new()
        {
            ["type"] = new OpenApiSchema
            {
                Description = "URI de identificação do problema",
                Type = StringType,
                Example = new OpenApiString(StatusCodesURI[StatusCode])
            },
            ["title"] = new OpenApiSchema
            {
                Description = "Título curto e legível do problema",
                Type = StringType,
                Example = new OpenApiString(StatusCodesTitle[StatusCode])
            },
            ["status"] = new OpenApiSchema
            {
                Description = "Código de status HTTP",
                Type = "integer",
                Format = "int32",
                Example = new OpenApiInteger(StatusCode)
            },
            ["detail"] = new OpenApiSchema
            {
                Description = "Mensagem detalhada do problema",
                Type = StringType,
                Example = new OpenApiString(StatusCodesDetails[StatusCode])
            },
            ["instance"] = new OpenApiSchema
            {
                Description = "URI de identificação do recurso que gerou o problema",
                Type = StringType,
                Example = new OpenApiString("/users")
            },
        };

        if (StatusCode == 400) fields.Add("errors", CreateErrorsSchema());

        return new OpenApiSchema
        {
            Type = "object",
            Properties = fields,
        };
    }
}