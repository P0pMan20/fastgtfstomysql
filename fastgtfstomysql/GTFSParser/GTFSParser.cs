using System.Globalization;

namespace FastGTFSImport;

public partial class GTFSParser
{
    private List<Table> _tablesToCreate = new List<Table>();

    private string _rootPath;
    private IDatabase _database;

    public GTFSParser(string rootPath, IDatabase db)
    {
        _rootPath = rootPath;
        _database = db;
    }
    // private int _numberOfTablesToCreate = 0;
    public void Parse()
    {
        // TODO: check how optimise LINQ.Contains is - could creating a hashmap and checking that be faster?
        // TODO: custom exceptions - some can be handled others are unrecoverable
        // TODO: colour types could be hex numbers instead of VARCHAR?
        // TODO: implement all conditionals within each file
        // Scan for required files, then check for conditionals
        Agency(Path.Join(_rootPath, "agency.txt"));
        Stops(Path.Join(_rootPath, "stops.txt"));
        Routes(Path.Join(_rootPath, "routes.txt"));
        Trips(Path.Join(_rootPath, "trips.txt"));
        StopTimes(Path.Join(_rootPath, "stop_times.txt"));

        bool CalendarExists = CheckFileExists("calendar.txt");        
        bool CalendarDatesExists = CheckFileExists("calendar_dates.txt");
        if (!CalendarExists && !CalendarDatesExists)
        {
            throw new Exception("Missing both schedule files - calendar.txt and calendar_dates.txt");
        }
        // Optional Required?
        if (CalendarExists)
        {
            Calendar(Path.Join(_rootPath, "calendar.txt"));
        }        
        if (CalendarDatesExists)
        {
            CalendarDates(Path.Join(_rootPath, "calendar_dates.txt"));

        }
        // Optional
        if (CheckFileExists("shapes.txt"))
        {
            Shapes(Path.Join(_rootPath, "shapes.txt"));
        }
        if (CheckFileExists("feed_info.txt"))
        {
            FeedInfo(Path.Join(_rootPath, "feed_info.txt"));
        }
        // Shapes();
        // FeedInfo();
        
        // Haven't implemented others
    }

    public void CreateTables()
    {
        foreach (Table table in _tablesToCreate)
        {
            _database.CreateTable(table);
        }
    }

    private bool CheckFileExists(string GTFSFile)
    {
        return File.Exists(Path.Join(_rootPath, GTFSFile));
    }
}