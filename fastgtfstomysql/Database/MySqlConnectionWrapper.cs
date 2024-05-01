using System.Text;
using MySqlConnector;

namespace FastGTFSImport;

public class MySqlConnectionWrapper : IDatabase, IDisposable
{
    private MySqlConnection _connection;
    private MySqlTransaction? _currentTransaction = null;
    public MySqlConnectionWrapper(string connectionString)
    {
        _connection = new MySqlConnection(connectionString);
        _connection.Open();
        // Console.WriteLine(_connection.State);
    }
    public void CreateTable(Table table)
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

        // Console.WriteLine(text);
    
        command.CommandText = text.ToString();
        if (_currentTransaction != null)
        {
            command.Transaction = _currentTransaction;
        }
        command.ExecuteNonQuery();
    }

    public void InsertRow(string table, string[] values)
    {
        MySqlCommand command = new MySqlCommand($"INSERT INTO {table} VALUES ({values.FlattenToText()})",_connection);
        if (_currentTransaction != null)
        {
            command.Transaction = _currentTransaction;
        }
        // Console.WriteLine(command.CommandText);
        command.ExecuteNonQuery();
        
    }
    // // works like insert except it deletes existing rows if they match primary keys and adds the 'updated' row
    // public void ReplaceRow(string table, string[] values)
    // {
    //     MySqlCommand command = new MySqlCommand($"REPLACE INTO {table} VALUES ({values.FlattenToText()})",_connection);
    //     if (_currentTransaction != null)
    //     {
    //         command.Transaction = _currentTransaction;
    //     }
    //     // Console.WriteLine(command.CommandText);
    //     command.ExecuteNonQuery();
    //     
    // }

    public void BeginTransaction()
    {
        if (_currentTransaction == null)
        {
            _currentTransaction = _connection.BeginTransaction();
        }
    }

    public void CommitTransaction()
    {
        // Console.WriteLine(_currentTransaction==null);
        _currentTransaction.Commit();
        // Console.WriteLine(_currentTransaction==null);
        _currentTransaction.Dispose();
        // Console.WriteLine(_currentTransaction==null);
        _currentTransaction = null;

    }
    public void RollbackTransaction()
    {
        _currentTransaction.Rollback();
    }

    public void Dispose()
    {
        _connection.Close();
    }
}