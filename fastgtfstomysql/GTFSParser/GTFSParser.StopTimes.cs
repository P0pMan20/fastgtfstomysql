namespace FastGTFSImport;

public partial class GTFSParser
{
    private void StopTimes(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("trip_id") ||
            !columns.Contains("stop_sequence"))
        {
            throw new Exception($"Missing Required File Columns - stop.txt");
        }
        // conditional required/forbidden
        
        _tablesToCreate.Add(new Table("stop_times", columns.Select((x) =>
            {

                return (x, "VARCHAR(255)");
            }
            ).ToArray(), new string[]{"trip_id", "stop_sequence"}));
    }
}