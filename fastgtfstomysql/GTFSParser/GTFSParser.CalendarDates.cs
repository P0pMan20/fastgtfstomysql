namespace FastGTFSImport;

public partial class GTFSParser
{
    private void CalendarDates(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("service_id") ||
            !columns.Contains("date") ||
            !columns.Contains("exception_type"))
        {
            throw new Exception($"Missing Required File Columns - calendar_dates.txt");
        }
        // PK service_id, date
        _tablesToCreate.Add(new Table("calendar_dates", columns.Select((x) =>
        {
            return (x, "VARCHAR(255)");
        }).ToArray(), new string[]{"service_id, date"}));
    }
}