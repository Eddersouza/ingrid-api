namespace IP.Shared.Persistence.Data;

public interface IApiDBSeeder
{
    void Migrate();

    void Seed();
}