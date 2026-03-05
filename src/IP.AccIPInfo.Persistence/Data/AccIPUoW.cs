namespace IP.AccIPInfo.Persistence.Data;

public interface IAccIPUoW : IUnitOfWork;

internal class AccIPInfoUoW(AccIPDbContext context) :
    UnitOfWork(context), IAccIPUoW;