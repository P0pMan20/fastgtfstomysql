namespace FastGTFSImport;

public partial class GTFSParser
{
    private void Shapes(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("shape_id") ||
            !columns.Contains("shape_pt_lat") ||
            !columns.Contains("shape_pt_lon") ||
            !columns.Contains("shape_pt_sequence"))
        {
            throw new Exception($"Missing Required File Columns - shapes.txt");
        }
        // PK shape_id, shape_pt_sequence
        _tablesToCreate.Add(new Table("shapes", columns.Select((x) =>
        {
            if (x == "shape_pt_lat" || x == "shape_pt_lon")
            {
                return (x, "DECIMAL(11,8)");
            }
            
            if (x == "shape_pt_sequence")
            {
                return (x, "INT");
            }
            
            return (x, "VARCHAR(255)");
        }).ToArray(), new string[]{"shape_pt_sequence","shape_id"}));
    }
}