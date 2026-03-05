namespace IP.AccCust.Persistence.Data;

public interface IAccCustUoW : IUnitOfWork;
internal class AccCustUoW(AccCustDbContext context) :
    UnitOfWork(context), IAccCustUoW;