namespace IP.Shared.Persistence.Repositories;

public class UnitOfWork(DbContext context) : IUnitOfWork
{    
    private static readonly Dictionary<Type, Type> _repositoryMap = BuildRepositoryMap();

    private readonly DbContext _context = context;
    private readonly Dictionary<Type, object> _repositories = [];

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public TRepository GetRepository<TRepository>()
                where TRepository : IRepository
    {
        if (_repositories.TryGetValue(typeof(TRepository), out var existing))
            return (TRepository)existing;

        if (!_repositoryMap.TryGetValue(typeof(TRepository), out var implementationType))
            throw new InvalidOperationException($"No implementation found for {typeof(TRepository).Name}");

        var repositoryInstance = (TRepository)Activator.CreateInstance(implementationType, _context)!;
        _repositories[typeof(TRepository)] = repositoryInstance;

        return repositoryInstance;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Dictionary<Type, Type> BuildRepositoryMap()
    {
        var interfaceType = typeof(IRepository);
        var assemblies = GetPersistenceAssemblies();
        var map = new Dictionary<Type, Type>();

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes().Where(IsRepositoryImplementation))
            {
                var interfaces = GetRepositoriesInterface(interfaceType, type);

                if (interfaces.Count == 1)
                {
                    map[interfaces[0]] = type;
                }
                else
                {
                    foreach (var repositoryInterface in interfaces.Where(interfaceType => !map.ContainsKey(interfaceType)))
                    {
                        map[repositoryInterface] = type;
                    }
                }
            }
        }

        return map;
    }

    private static List<Type> GetRepositoriesInterface(Type interfaceType, Type type)
    {
        return [.. type.GetInterfaces()
            .Where(i => i != typeof(IRepository) &&
                interfaceType.IsAssignableFrom(i))];
    }

    private static IEnumerable<Assembly> GetPersistenceAssemblies() =>
        AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name?
            .Contains("Persistence", StringComparison.OrdinalIgnoreCase) == true);

    private static bool IsRepositoryImplementation(Type type) =>
        type.IsClass &&
        !type.IsAbstract &&
        typeof(IRepository).IsAssignableFrom(type);
}