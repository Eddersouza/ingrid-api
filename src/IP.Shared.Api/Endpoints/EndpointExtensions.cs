namespace IP.Shared.Api.Endpoints;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly)
    {
        ServiceDescriptor[] endpoints = [.. assembly
        .DefinedTypes
        .Where(type => type is { IsAbstract: false, IsInterface: false } &&
            type.IsAssignableTo(typeof(IEndpoint)))
        .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))];

        services.TryAddEnumerable(endpoints);

        return services;
    }

    public static RouteHandlerBuilder IsDeprecated(this RouteHandlerBuilder builder)
    {
        return builder.WithMetadata(new DeprecatedEndpointMetadata());
    }

    public static RouteHandlerBuilder MapEndpointDescription(
        this RouteHandlerBuilder app,
        string summary,
        string description,
        params string[] tags) =>
            app.WithSummary(summary)
                .WithDescription(description)
                .WithTags(tags);

    public static RouteHandlerBuilder MapEndpointProduces<TSuccess>(
        this RouteHandlerBuilder app, bool isCreate = false)
    {
        app.Produces<TSuccess>(isCreate ? StatusCodes.Status201Created : StatusCodes.Status200OK);
        app.Produces(StatusCodes.Status400BadRequest);
        app.Produces(StatusCodes.Status404NotFound);
        app.Produces(StatusCodes.Status409Conflict);
        app.Produces(StatusCodes.Status422UnprocessableEntity);
        app.Produces(StatusCodes.Status500InternalServerError);

        return app;
    }

    public static RouteHandlerBuilder MapEndpointProducesAuth(
        this RouteHandlerBuilder app)
    {
        app.Produces(StatusCodes.Status401Unauthorized);
        app.Produces(StatusCodes.Status403Forbidden);

        return app;
    }

    public static IApplicationBuilder MapEndpoints(
                this WebApplication app,
        string routePrefix,
        List<EndpointVersion> endpointVersions,
        Assembly assembly)
    {
        var apiVersionSet = app.NewApiVersionSet();

        foreach (var item in endpointVersions)
            apiVersionSet = apiVersionSet.HasApiVersion(new ApiVersion(item.Version));

        routePrefix = routePrefix.IsNotNullOrWhiteSpace() ?
            $"/{routePrefix}" :
            string.Empty;

        RouteGroupBuilder groupBuilder =
            app.MapGroup(routePrefix + "/v{version:apiVersion}")
            .AddEndpointFilter<LogPathFilter>()
            .WithApiVersionSet(apiVersionSet.Build());

        var interfaceTypes = assembly.FindDerivedType(typeof(IEndpoint));

        IEnumerable<IEndpoint> endpoints =
           app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder =
            groupBuilder is null ? app : groupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            var isOk = interfaceTypes.IsAssignableFrom(endpoint.GetType());
            if (isOk)
                endpoint.MapEndpoint(builder);
        }

        return app;
    }

    public static RouteHandlerBuilder MapEndpointVersions(
        this RouteHandlerBuilder app,
        params int[] version)
    {
        foreach (var v in version) app.MapToApiVersion(v);

        return app;
    }
}