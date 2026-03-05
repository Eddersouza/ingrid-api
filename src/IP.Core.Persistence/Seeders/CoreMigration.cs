namespace IP.Core.Persistence.Seeders;

internal sealed class CoreMigration(
    CoreDbContext _context) : IApiDBSeeder
{
    private DbSet<State> _stateSet = default!;
    private DbSet<City> _citySet = default!;

    public void Migrate() =>
       _context.Database.Migrate();

    public void Seed()
    {
        _stateSet = _context.Set<State>();
        _citySet = _context.Set<City>();

        if (!_stateSet.Any()) CreateStates();

        if (!_citySet.Any()) CreateCities();
    }

    private void CreateCities()
    {
        foreach (var cities in CityData.Entities(StateData.CodeGuide))
        {
            _citySet.AddRange(cities);
        }

        _context.SaveChanges();
    }

    private void CreateStates()
    {
        var states = StateData.Entities();
        _stateSet.AddRange(states);

        _context.SaveChanges();
    }
}