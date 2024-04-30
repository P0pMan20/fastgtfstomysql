namespace FastGTFSImport;

public interface IDatabase
{
    public bool InsertRow(string table, string[] values);

    public bool CreateTable(string tableName, ValueTuple<string, string>[] ColumnsAndDataTypes);

}