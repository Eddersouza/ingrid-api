namespace IP.Com.Persistence.Seeders;

internal sealed class ComMigration(
    ComDbContext _context) : IApiDBSeeder
{
    public void Migrate() =>
        _context.Database.Migrate();

    public void Seed()
    { }
}