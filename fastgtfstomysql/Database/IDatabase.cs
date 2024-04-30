namespace FastGTFSImport;

public interface IDatabase
{
    // success/fail
    
    bool InsertRow(string table, string[] values);

    bool CreateTable(string tableName, Tuple<string, string>[] ColumnsAndDataTypes);

}