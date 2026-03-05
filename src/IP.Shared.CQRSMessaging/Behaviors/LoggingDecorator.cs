namespace IP.Shared.CQRSMessaging.Behaviors;

internal static class LogguerCQRSMessaging
{
    private static readonly JsonSerializerOptions options =
        new() { WriteIndented = true };

    public static void AddCQRSFailureLogging<TCommand>(this ILogger logger, TCommand command, Result result)
    {
        string dataError = result.Error switch
        {
            ErrorValidation validation => JsonSerializer.Serialize(validation, options),
            _ => JsonSerializer.Serialize(result.Error, options)
        };
        logger.LogWarning("Completed command {CommandName} with error {ErrorData}", typeof(TCommand).Name, dataError);
    }

    public static void AddCQRSFailureLogging<TCommand, TResponse>(this ILogger logger, TCommand command, Result<TResponse> result)
    {
        string dataError = result.Error switch
        {
            ErrorValidation validation => JsonSerializer.Serialize(validation, options),
            _ => JsonSerializer.Serialize(result.Error, options)
        };
        logger.LogWarning("Completed command {CommandName} with error {ErrorData}", typeof(TCommand).Name, dataError);
    }

    public static void AddCQRSStartLogging<TCommand>(this ILogger logger, TCommand command)
    {
        var isLoggableData = command is ILoggableData;

        if (isLoggableData)
        {
            var maskedObj = LogInterceptor.MaskFields(command);
            string data = JsonSerializer.Serialize(maskedObj, options);
            logger.LogInformation("Processing command {CommandName} with {@LoggableData}", typeof(TCommand).Name, data);
        }
        else
            logger.LogInformation("Processing command {CommandName}", typeof(TCommand).Name);
    }

    public static void AddCQRSSuccessLogging<TCommand>(this ILogger logger, TCommand command)
    {
        logger.LogInformation("Completed command {CommandName}", typeof(TCommand).Name);
    }
}

internal static class LoggingDecorator
{
    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> inner,
        ILogger<CommandBaseHandler<TCommand>> logger)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            logger.AddCQRSStartLogging(command);

            Result result = await inner.Handle(command, cancellationToken);

            if (result.IsSuccess) logger.AddCQRSSuccessLogging(command);
            else logger.AddCQRSFailureLogging(command, result);

            return result;
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> inner,
        ILogger<CommandHandler<TCommand, TResponse>> logger)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            logger.AddCQRSStartLogging(command);

            Result<TResponse> result = await inner.Handle(command, cancellationToken);

            if (result.IsSuccess) logger.AddCQRSSuccessLogging(command);
            else logger.AddCQRSFailureLogging(command, result);

            return result;
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> inner,
        ILogger<QueryHandler<TQuery, TResponse>> logger)
        : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string data = JsonSerializer.Serialize(query, options);

            logger.LogInformation("Processing query {QueryName} with data {@LoggableData}", typeof(TQuery).Name, data);

            Result<TResponse> result = await inner.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                logger.LogInformation("Completed query {QueryName}", typeof(TQuery).Name);
            }
            else
            {
                string dataError = result.Error switch
                {
                    ErrorValidation validation => JsonSerializer.Serialize(validation, options),
                    _ => JsonSerializer.Serialize(result.Error, options)
                };
                logger.LogWarning("Completed command {QueryName} with error {@ErrorData}", typeof(TQuery).Name, dataError);
            }

            return result;
        }
    }
}