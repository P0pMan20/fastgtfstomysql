namespace FastGTFSImport;

public partial class GTFSParser
{
    private void Calendar(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("service_id") ||
            !columns.Contains("monday") ||
            !columns.Contains("tuesday") ||
            !columns.Contains("wednesday") ||
            !columns.Contains("thursday") ||
            !columns.Contains("friday") ||
            !columns.Contains("saturday") ||
            !columns.Contains("sunday") ||
            !columns.Contains("start_date") ||
            !columns.Contains("end_date"))
        {
            throw new Exception($"Missing Required File Columns - calendar.txt");
        }
        // PK service_id
        _tablesToCreate.Add(new Table("calendar", columns.Select((x) =>
        {
            return (x, "VARCHAR(255)");
        }).ToArray(), new string[]{"service_id"}));
    }
}