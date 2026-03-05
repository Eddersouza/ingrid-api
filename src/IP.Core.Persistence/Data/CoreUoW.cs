namespace IP.Core.Persistence.Data;

public interface ICoreUoW : IUnitOfWork;

internal class CoreUoW(CoreDbContext context) :
    UnitOfWork(context), ICoreUoW;