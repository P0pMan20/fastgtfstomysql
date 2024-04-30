namespace FastGTFSImport;

public partial class GTFSParser
{
    private void Trips(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("route_id") ||
            !columns.Contains("trip_id") ||
            !columns.Contains("service_id"))
        {
            throw new Exception($"Missing Required File Columns - trips.txt");
        }
        _tablesToCreate.Add(new Table("trips", columns.Select((x) =>
        {
            
            
            return (x, "VARCHAR(255)");
        }).ToArray(), new string[]{"trip_id"}));
    }
}