namespace IP.Core.Persistence.States;

public interface IStateRepository : 
    IQueryableRepository<State>,
    ICreationRepository<State>,
    IUpdatableRepository<State>,
    IDeletableRepository<State>;

internal sealed class StateRepository(CoreDbContext appContext) :
    RepositoryBase<State>(appContext),
    IStateRepository;
