namespace FastGTFSImport;

public partial class GTFSParser
{
    private void FeedInfo(string path)
    {
        string[] columns = CSVParser.ParseColumns(path);

        if (!columns.Contains("feed_publisher_name") ||
            !columns.Contains("feed_publisher_url") ||
            !columns.Contains("feed_lang"))
        {
            throw new Exception($"Missing Required File Columns - feed_info.txt");
        }
        // no PK
        _tablesToCreate.Add(new Table("feed_info", columns.Select((x) =>
        {
            return (x, "VARCHAR(255)");
        }).ToArray(), Array.Empty<string>()));

    }
}