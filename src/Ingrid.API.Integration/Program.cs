using IP.AccCust.Api;
using IP.AccCust.Workers;
using IP.Shared.Abstractions.Sessions;
using IP.Shared.Api;
using IP.Shared.Api.Documents;
using IP.Shared.Api.Endpoints;
using IP.Shared.Api.Logging;
using IP.Shared.CQRSMessaging;
using IP.Shared.IntegrationEvents;
using IP.Shared.IP;
using IP.Shared.Persistence;
using IP.Shared.Security;
using Quartz;

namespace Ingrid.API.Integration;

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

        builder.Services.AddLogPathFilter();

        builder.Services.AddOnzVPN(builder.Configuration);

        builder.Services.AddCQRSMessaging([
           IP.AccCust.Api.AssemblyReference.Assembly,
        ]);

        builder.Services.AddInMemoryEventBus([
            IP.AccCust.Api.AssemblyReference.Assembly,
        ]);

        builder.Services.AddApiDocumentation(builder.Configuration, endpointVersions);
        builder.Services.AddEndpoints(IP.AccCust.Api.AssemblyReference.Assembly);
        
        builder.Services.AddDBInterceptors();

        builder.Services.AddAccCustServices();
        builder.Services.AddPersistence(
            builder.Configuration,
            IP.AccCust.Persistence.AssemblyReference.Assembly);

        builder.Services.AddQuartz();

        builder.Services.AddQuartzHostedService(service =>
        {
            service.WaitForJobsToComplete = true;
        });
        builder.Services.AddAccCustWorkers(builder.Configuration);

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
        app.MapEndpoints("integra", endpointVersions, IP.AccCust.Api.AssemblyReference.Assembly);
        app.MapApiDocumentation("Ingrid Api Integration", endpointVersions);

        app.Run();
    }
}