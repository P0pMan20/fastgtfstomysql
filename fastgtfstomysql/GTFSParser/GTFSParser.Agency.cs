namespace FastGTFSImport;

public partial class GTFSParser
{
    private bool MultipleAgencyFlag = true;
    private void Agency(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("agency_name") ||
            !columns.Contains("agency_url") ||
            !columns.Contains("agency_timezone"))
        {
            throw new Exception($"Missing Required File Columns - agency.txt");
        }
        if (!columns.Contains("agency_id"))
        {
            Console.WriteLine("potentially broken file - missing agency_id, could be due to only having one transport agency");
            MultipleAgencyFlag = false;
        }
        // all fields are text so they can all be varchars
        // PK agency_id
        _tablesToCreate.Add(new Table("agency",columns.Select((x) => (
             (x, "VARCHAR(255)")
            )).ToArray(), columns.Contains("agency_id") ? new string[] {"agency_id"} : Array.Empty<string>() ));

    }
}