namespace IP.Shared.CQRSMessaging.Messages;

public interface IQueryHandler<in TQuery, TResponse>
{
    Task<Result<TResponse>> Handle(
        TQuery query,
        CancellationToken cancellationToken);
}