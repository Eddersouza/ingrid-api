namespace IP.Shared.CQRSMessaging.Messages;

public interface IBaseCommand;

public interface ICommand<TResponse> : IBaseCommand;

public interface ICommand : ICommand<Result>;