namespace IP.Shared.Api.Logging;

public static class OpenTelemetryStartupExtension
{
    public static WebApplicationBuilder AddLogTelemetry(this WebApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
            options.AddOtlpExporter();
        });

        return builder;
    }

    public static OpenTelemetryBuilder AddMetricTelemetry(this OpenTelemetryBuilder openTelemetryBuilder)
    {
        openTelemetryBuilder.WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation();
            metrics.AddHttpClientInstrumentation();
            metrics.AddOtlpExporter();
        });

        return openTelemetryBuilder;
    }

    public static OpenTelemetryBuilder AddTracingTelemetry(this OpenTelemetryBuilder openTelemetryBuilder)
    {
        openTelemetryBuilder.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddEntityFrameworkCoreInstrumentation();
            tracing.AddConnectorNet();
            tracing.AddOtlpExporter();
        });

        return openTelemetryBuilder;
    }
}