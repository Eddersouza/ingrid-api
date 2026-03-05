namespace IP.Shared.CQRSMessaging.Messages;

public interface IQuery<TResponse>;

public abstract class QueryBaseFilter
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }}