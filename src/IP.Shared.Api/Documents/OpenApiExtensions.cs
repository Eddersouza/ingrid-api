namespace IP.Shared.Api.Documents;

public static class OpenApiExtensions
{
    public static IServiceCollection AddApiDocumentation(
        this IServiceCollection services,
        IConfiguration configuration,
        List<EndpointVersion> versions)
    {
        AddOpenApiDocuments(services, configuration, versions);
        AddApiVersioning(services);

        return services;
    }

    public static IApplicationBuilder MapApiDocumentation(
        this WebApplication app,
        string documentTitle,
       List<EndpointVersion> versions)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options => options
            .WithTitle(documentTitle)
            .WithTheme(ScalarTheme.Moon)
            .WithTagSorter(TagSorter.Alpha)
            .WithOperationSorter(OperationSorter.Alpha)
            .AddDocuments(versions.Select(x => x.VersionPattern)));

        return app;
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = ApiVersion.Default;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    private static void AddOpenApiDocuments(
        IServiceCollection services,
        IConfiguration configuration,
        List<EndpointVersion> versions)
    {
        services.Configure<ApiDocumentInfoOptions>(
           configuration.GetSection(ApiDocumentInfoOptions.NameKey));

        foreach (var version in versions)
        {
            services.AddOpenApi(version.VersionPattern, options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                options.AddDocumentTransformer<ApiDocumentTransformer>();
                options.AddOperationTransformer<DeprecatedOperationTransformer>();
                options.AddOperationTransformer<OKResponseTransformer>();
                options.AddOperationTransformer<CreatedResponseTransformer>();
                options.AddOperationTransformer<BadRequestResponseTransformer>();
                options.AddOperationTransformer<UnauthorizedResponseTransformer>();
                options.AddOperationTransformer<ForbiddenResponseTransformer>();
                options.AddOperationTransformer<NotFoundResponseTransformer>();
                options.AddOperationTransformer<ConflictResponseTransformer>();
                options.AddOperationTransformer<UnprocessableEntityResponseTransformer>();
                options.AddOperationTransformer<InternalServerErrorResponseTransformer>();
            });
        }
    }
}