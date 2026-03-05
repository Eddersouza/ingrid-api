namespace IP.Shared.CQRSMessaging.Behaviors;

internal static class ValidationDecorator
{
    private static ErrorValidation CreateValidationError(ValidationFailure[] validationFailures) =>
        new([.. validationFailures.Select(f => Error.Failure(f.ErrorCode, f.ErrorMessage))]);

    internal sealed class CommandBaseHandler<TCommand>(
            ICommandHandler<TCommand> inner,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand> where TCommand : ICommand
    {
        public async Task<Result> Handle(
            TCommand command,
            CancellationToken cancellationToken)
        {
            ValidationResult[] validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(
                    new ValidationContext<TCommand>(command),
                cancellationToken)));

            ValidationFailure[] failures = [.. validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)];

            return failures.Any() ?
                Result.Failure(CreateValidationError(failures)) :
                await inner.Handle(command, cancellationToken);
        }
    }

    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> inner,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            ValidationResult[] validationResults = await Task.WhenAll(
               validators.Select(v => v.ValidateAsync(
                   new ValidationContext<TCommand>(command),
               cancellationToken)));

            ValidationFailure[] failures = [.. validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)];

            return failures.Any() ?
                Result.Failure<TResponse>(CreateValidationError(failures)) :
                await inner.Handle(command, cancellationToken);
        }
    }
}