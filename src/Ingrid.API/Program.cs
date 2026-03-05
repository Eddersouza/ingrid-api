using Ingrid.API.Workers.Seed;
using IP.Com.Workers;
using IP.Shared.Abstractions.Sessions;
using IP.Shared.Api;
using IP.Shared.Api.Documents;
using IP.Shared.Api.Endpoints;
using IP.Shared.Api.Logging;
using IP.Shared.CQRSMessaging;
using IP.Shared.Emails;
using IP.Shared.IntegrationEvents;
using IP.Shared.Persistence;
using IP.Shared.Security;
using IP.Shared.EndpointModules;
using Quartz;
using IP.AccIPInfo.Api;

namespace Ingrid.API;

public class Program
{
    public static void Main(string[] args)
    {
        List<EndpointVersion> endpointVersions = [new EndpointVersion(1)];

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        builder.AddSerilog();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddCurrentSession();
        builder.Services.AddAuthenticationInternal(builder.Configuration);
        builder.Services.AddAuthorizationInternal();
        builder.Services.AddApiOptions(builder.Configuration);

        builder.Services.AddLogPathFilter();

        builder.Services.AddCQRSMessaging([
            IP.IDI.Api.AssemblyReference.Assembly,
            IP.Core.Api.AssemblyReference.Assembly,
            IP.Com.Api.AssemblyReference.Assembly,
            IP.AccIPInfo.Api.AssemblyReference.Assembly,
        ]);

        builder.Services.AddInMemoryEventBus([
            IP.Com.Api.AssemblyReference.Assembly,
        ]);

        builder.Services.AddApiDocumentation(builder.Configuration, endpointVersions);

        builder.Services.AddEndpoints(IP.IDI.Api.AssemblyReference.Assembly);
        builder.Services.AddEndpoints(IP.Core.Api.AssemblyReference.Assembly);
        builder.Services.AddEndpoints(IP.AccIPInfo.Api.AssemblyReference.Assembly);

        builder.Services.AddDBInterceptors();

        builder.Services.AddPersistence(
            builder.Configuration,
            IP.IDI.Persistence.AssemblyReference.Assembly);

        builder.Services.AddPersistence(
            builder.Configuration,
            IP.Core.Persistence.AssemblyReference.Assembly);

        builder.Services.AddPersistence(
           builder.Configuration,
           IP.Com.Persistence.AssemblyReference.Assembly);

        builder.Services.AddPersistence(
            builder.Configuration,
            IP.AccIPInfo.Persistence.AssemblyReference.Assembly);

        builder.Services.ExecuteMigrations([
            IP.IDI.Persistence.AssemblyReference.Assembly,
            IP.Core.Persistence.AssemblyReference.Assembly,
            IP.Com.Persistence.AssemblyReference.Assembly,
            IP.AccIPInfo.Persistence.AssemblyReference.Assembly
        ]);

        builder.Services.AddQuartz();

        builder.Services.AddQuartzHostedService(service =>
        {
            service.WaitForJobsToComplete = true;
        });

        builder.Services.AddSeederWorker(builder.Configuration);
        builder.Services.AddComWorkers(builder.Configuration);
        builder.Services.AddEmailService(builder.Configuration);

        builder.Services.AddEndpointModuleServices(builder.Configuration);
        builder.Services.AddAccountInfoServices();

        var app = builder.Build();
        app.UseCors();

        app.Use(async (context, next) =>
        {
            if (context.Request.Method == HttpMethods.Options)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                await context.Response.CompleteAsync();
                return;
            }
            await next();
        });

        app.UseSerilog();

        app.UseExceptionHandler();

        app.UseAuthorization();

        app.MapEndpoints("idi", endpointVersions, IP.IDI.Api.AssemblyReference.Assembly);
        app.MapEndpoints("core", endpointVersions, IP.Core.Api.AssemblyReference.Assembly);
        app.MapEndpoints("com", endpointVersions, IP.Com.Api.AssemblyReference.Assembly);
        app.MapEndpoints("acc-ip-info", endpointVersions, IP.AccIPInfo.Api.AssemblyReference.Assembly);
        app.MapApiDocumentation("Ingrid Api", endpointVersions);
        app.MapGet("/health", () => Results.Ok("healthy"));
        app.Run();
    }
}