namespace IP.Shared.Persistence.Interceptors;

internal class MySqlConnectionInterceptor(string databaseName) :
    DbConnectionInterceptor
{
    private readonly string database = databaseName;

    public override void ConnectionOpened(
        DbConnection connection,
        ConnectionEndEventData eventData)
    {
        if (database != null)
        {
            connection.ChangeDatabase(database);
        }
        base.ConnectionOpened(connection, eventData);
    }

    public override async Task ConnectionOpenedAsync(
        DbConnection connection,
        ConnectionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (database != null)
            await connection.ChangeDatabaseAsync(database, cancellationToken);

        await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
    }
}