namespace IP.AccIPInfo.Persistence.Seeders;

internal sealed class CoreMigration(
    AccIPDbContext _context) : IApiDBSeeder
{
    public void Migrate() =>
       _context.Database.Migrate();

    public void Seed()
    { }
}