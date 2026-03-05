namespace IP.IDI.Persistence.Data;

public interface IIDIUnitOfWork : IUnitOfWork;

internal class IDIUnitOfWork(IDIDbContext context) :
    UnitOfWork(context), IIDIUnitOfWork;