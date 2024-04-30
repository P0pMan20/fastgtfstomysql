using System.Diagnostics;
using System.Text;

namespace FastGTFSImport;

class Program
{
    // Scan for files -> Create table headings -> Create tables if desired -> parse whole file -> insert (threaded)
    static void Main(string[] args)
    {
        string rootPath = "C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\CNS_GTFS";
        // TODO: some sort of argument parsing?
        // string[] file = File.ReadAllLines("C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\stop_times_trunc.txt");
        // string[] file = File.ReadAllLines("C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\stop_times.txt");
        // CSVParser parser = new CSVParser();
        // Stopwatch watch = new Stopwatch();
        // parser.ParseFile(file, 16);
        IDatabase db = new MySqlConnectionWrapper("Server=localhost;User ID=test;Password=pass;Database=test;Port=3306;Protocol=Socket");
        GTFSParser gtfs = new GTFSParser(rootPath, db);
        gtfs.Parse();
        Console.WriteLine("parsed");
        gtfs.CreateTables();

    }

    private static void ShowHelp()
    {
        Console.WriteLine("tbd");
    }
}

