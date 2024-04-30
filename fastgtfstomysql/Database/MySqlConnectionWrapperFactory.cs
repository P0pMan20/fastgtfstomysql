namespace FastGTFSImport;

public class MySqlConnectionWrapperFactory : IDatabaseFactory
{
    public string connectionString;
    public MySqlConnectionWrapperFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public IDatabase Produce()
    {
        return new MySqlConnectionWrapper(connectionString);
    }
}