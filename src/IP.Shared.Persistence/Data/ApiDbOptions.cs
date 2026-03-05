namespace IP.Shared.Persistence.Data;

public class ApiDbOptions
{
    public const string NameKey = "api.db";

    public string DefaultConnectionString { get; set; } = string.Empty;

    public List<ApiDbConnectionOptions> Connections { get; set; } = [];
}

public class ApiDbConnectionOptions
{
    public string Id { get; set; } = string.Empty;
    public string Connection { get; set; } = string.Empty;
    public ApiDbConnectionOptionsEnum Provider { get; set; } = ApiDbConnectionOptionsEnum.Mysql;
}

public enum ApiDbConnectionOptionsEnum
{
    Mysql,
    Postgres
}
