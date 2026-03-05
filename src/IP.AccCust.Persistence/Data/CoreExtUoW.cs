namespace IP.AccCust.Persistence.Data;

public interface ICoreExtUoW : IUnitOfWork;

internal class CoreExtUoW(CoreExtDbContext context) :
    UnitOfWork(context), ICoreExtUoW;