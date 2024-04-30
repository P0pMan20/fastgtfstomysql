namespace FastGTFSImport;

public interface IDatabaseFactory
{
    // public string connectionString;
    // public IDatabaseFactory(string connectionString);
    IDatabase Produce();
};