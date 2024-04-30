namespace FastGTFSImport;

public interface IDatabase
{
    public void BeginTransaction();
    public void CommitTransaction();
    public void InsertRow(string table, string[] values);

    public void CreateTable(Table table);

}