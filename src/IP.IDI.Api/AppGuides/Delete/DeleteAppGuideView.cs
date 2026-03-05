namespace IP.IDI.Api.AppGuides.Delete;

public sealed record DeleteAppGuideCommand(Guid Id) :
    ICommand<DeleteAppGuideResponse>, ILoggableData;

public sealed class DeleteAppGuideReques;

public sealed class DeleteAppGuideResponse(string message) :
    ResolvedData<object>(null, message);