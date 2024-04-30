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
    public bool CreateTable(Table table)
    {
        var command = new MySqlCommand();
        command.Connection = _connection;
        StringBuilder text = new StringBuilder($"CREATE TABLE {table.Name} (");
        foreach (var column in table.ColumnNamesAndTheirCorrespondingDatatype)
        {
            text.Append($"{column.Item1} {column.Item2},");
        }

        if (table.PrimaryKeys is not [])
        {
            text.Append("PRIMARY KEY (");
            foreach (var primaryKey in table.PrimaryKeys)
            {
                text.Append($"{primaryKey},");
            }
            text.Remove(text.Length-1, 1);
            text.Insert(text.Length, "));");
        }
        else
        {
            text.Remove(text.Length-1, 1);
            text.Insert(text.Length, ");");
        }

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