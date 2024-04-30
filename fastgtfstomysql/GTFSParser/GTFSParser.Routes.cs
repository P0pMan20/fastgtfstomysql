namespace FastGTFSImport;

public partial class GTFSParser
{
    private void Routes(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("route_id") || 
            !columns.Contains("route_type"))
        {
            throw new Exception($"Missing Required File Columns - route.txt");
        }
        if (!columns.Contains("agency_id") && MultipleAgencyFlag)
        {   
            throw new Exception($"Missing Required File Columns - route.txt");
        }
        // handle route name
        if (!columns.Contains("route_short_name") && !columns.Contains("route_long_name"))
        {
            throw new Exception($"Missing Required File Columns, no route name - route.txt");
        }
        if (columns.Contains("network_id") && CheckFileExists("route_networks.txt"))
        {
            throw new Exception($"Forbidden file column, network_id is present when route_networks.txt exists - route.txt");
        }
        // route_id
        // GTFS Enums don't really correspond to MySQL Enums
        _tablesToCreate.Add(new Table("routes", columns.Select((x) =>
        {

            if (x == "route_id")
            {
                return (x, "VARCHAR(255) PRIMARY KEY");
            }
            
            return (x, "VARCHAR(255)");
        }).ToArray()));
    }
    
}