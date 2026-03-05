namespace IP.AccCust.Persistence.PlansView;

public interface IPlanViewRepository : IQueryableRepository<PlanView>;

internal sealed class PlanViewRepository(AccCustDbContext appContext) :
    RepositoryBase<PlanView>(appContext),
    IPlanViewRepository;