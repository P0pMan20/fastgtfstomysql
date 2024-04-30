namespace FastGTFSImport;

public partial class GTFSParser
{
    private void Stops(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("stop_id"))
        {
            throw new Exception($"Missing Required File Columns - stop.txt");
        }
        if (!columns.Contains("zone_id") && CheckFileExists("fare_rules.txt"))
        {
            throw new Exception($"Missing Required File Columns - stop.txt");
        }
        // TODO: handle potentially lack of conditional requirements?
        // PK stop_id
        _tablesToCreate.Add(new Table("stops", columns.Select((x) =>
        {
            if (x == "stop_lat" || x == "stop_lon")
            {
                return (x, "DECIMAL(11,8)");
            }

            if (x == "stop_id")
            {
                return (x, "VARCHAR(255) PRIMARY KEY");
            }
            
            return (x, "VARCHAR(255)");
        }).ToArray()));


    }
    
}