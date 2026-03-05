namespace IP.AccCust.Persistence.Data;

public interface IAccIPExtUoW : IUnitOfWork;

internal class AccIPInfoExtUoW(AccIPExtDbContext context) :
    UnitOfWork(context), IAccIPExtUoW;