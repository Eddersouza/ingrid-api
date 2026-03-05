namespace IP.Shared.Security;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthConfigurationOptions>(
            configuration.GetSection(AuthConfigurationOptions.NameKey));

        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        AuthConfigurationOptions authOptions =
            configuration.GetSection(AuthConfigurationOptions.NameKey)
            .Get<AuthConfigurationOptions>()
            ?? throw new ArgumentNullException(
                nameof(configuration),
                "AuthConfigurationOptions section is missing or invalid.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = CreateValidationToken(authOptions);
            options.MapInboundClaims = false;
            options.Events = new JwtBearerEvents
            {
                OnChallenge = HandleChallengeContext(),
                OnForbidden = HandleForbbidenContext()
            };
        });

        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection AddAuthorizationInternal(
        this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

        var claims = ClaimsInfo.GetPermissionList();

        foreach (var claim in claims)
        {
            services.AddAuthorizationBuilder()
                .AddPermissionPolicy(
                    claim.Key,
                    [.. claim.Value.Select(x => x.Claim)]);
        }

        return services;
    }

    private static AuthorizationBuilder AddPermissionPolicy(
            this AuthorizationBuilder policyBuilder,
        string policyName,
        string[] claims)
    {
        return policyBuilder
            .AddPolicy(policyName, policy =>
            policy.AddRequirements(new PermissionRequirement(claims)));
    }

    private static TokenValidationParameters CreateValidationToken(
                AuthConfigurationOptions authOptions) =>
        new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(authOptions.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };

    private static Func<JwtBearerChallengeContext, Task> HandleChallengeContext()
    {
        return async context =>
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "É necessário entrar no sistema para acessar esse recurso.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                Instance = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
            context.HandleResponse();
        };
    }

    private static Func<ForbiddenContext, Task> HandleForbbidenContext()
    {
        return async context =>
        {
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Detail = "É necessário ter permissão para acessar esse recurso.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                Instance = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        };
    }
}