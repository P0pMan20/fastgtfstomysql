using System.Text;
using MySqlConnector;

namespace FastGTFSImport;

public class MySqlConnectionWrapper : IDatabase, IDisposable
{
    private MySqlConnection _connection;
    public MySqlConnectionWrapper(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        Console.WriteLine(_connection.State);
    }
    // current issue cant work with multiple primary keys - need minor refactor
    public bool CreateTable(string tableName, ValueTuple<string, string>[] ColumnsAndDataTypes)
    {
        var command = new MySqlCommand();
        command.Connection = _connection;
        StringBuilder text = new StringBuilder($"CREATE TABLE {tableName} (");
        foreach (var column in ColumnsAndDataTypes)
        {
            text.Append($"{column.Item1} {column.Item2},");
        }

        text.Remove(text.Length-1, 1);
        text.Insert(text.Length, ");");
        Console.WriteLine(text);

        command.CommandText = text.ToString();
        command.ExecuteNonQuery();
        return true;
    }

    public bool InsertRow(string table, string[] values)
    {
        return false;
    }

    public void Dispose()
    {
        _connection.Close();
    }
}