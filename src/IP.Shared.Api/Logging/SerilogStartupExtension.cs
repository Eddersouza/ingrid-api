namespace IP.Shared.Api.Logging;

public static class SerilogStartupExtension
{
    public static WebApplicationBuilder AddSerilog(
        this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom
            .Configuration(builder.Configuration)
            .WriteToConsole()
            .WriteToAmazonCloudWatch(builder)
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }

    public static WebApplication UseSerilog(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }

    internal static LoggerConfiguration WriteToAmazonCloudWatch(
        this LoggerConfiguration loggerConfiguration, WebApplicationBuilder builder)
    {
        builder.Services.Configure<AWSLogOptions>(
            builder.Configuration.GetSection(AWSLogOptions.NameKey));

        AWSLogOptions awsLogOptions =
          builder.Configuration.GetSection(AWSLogOptions.NameKey)
          .Get<AWSLogOptions>()
          ?? throw new ArgumentNullException(
              AWSLogOptions.NameKey,
              "AuthConfigurationOptions section is missing or invalid.");

        AmazonCloudWatchLogsConfig amazonCloudWatchLogsConfig = new();
        amazonCloudWatchLogsConfig.RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsLogOptions.Region);

        if (awsLogOptions.ServiceUrl.IsNotNullOrWhiteSpace())
            amazonCloudWatchLogsConfig.ServiceURL = awsLogOptions.ServiceUrl;

        AmazonCloudWatchLogsClient client = new(
            new Amazon.Runtime.BasicAWSCredentials(
                awsLogOptions.AccessKey,
                awsLogOptions.SecretKey),
            amazonCloudWatchLogsConfig
            );

        loggerConfiguration.WriteTo.AmazonCloudWatch(
            logGroup: awsLogOptions.LogGroupName,
            logStreamPrefix: awsLogOptions.LogStreamPrefix,
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
            createLogGroup: true,
            appendUniqueInstanceGuid: false,
            appendHostName: false,
            logGroupRetentionPolicy: LogGroupRetentionPolicy.FourMonths,
            cloudWatchClient: client);

        return loggerConfiguration;
    }

    internal static LoggerConfiguration WriteToConsole(
                this LoggerConfiguration loggerConfiguration)
    {
        string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        loggerConfiguration.WriteTo.Console(outputTemplate: outputTemplate);

        return loggerConfiguration;
    }
}