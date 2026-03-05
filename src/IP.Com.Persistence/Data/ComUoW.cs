namespace IP.Com.Persistence.Data;

public interface IComUoW : IUnitOfWork;

internal class ComUoW(ComDbContext context) :
    UnitOfWork(context), IComUoW;