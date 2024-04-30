using System.Diagnostics;
using System.Text;

namespace FastGTFSImport;

class Program
{
    // Scan for files -> Create table headings -> Create tables if desired -> parse whole file -> insert (threaded)
    static void Main(string[] args)
    {
        Thread.CurrentThread.Name = "Main";
        string rootPath = "C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\CNS_GTFS";
        IDatabaseFactory db = new MySqlConnectionWrapperFactory("Server=localhost;User ID=test;Password=pass;Database=test;Port=3306;Protocol=Socket");
        // string rootPath = "C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\SEQ_SCH_GTFS";
        // IDatabaseFactory db = new MySqlConnectionWrapperFactory("Server=localhost;User ID=test;Password=pass;Database=test2;Port=3306;Protocol=Socket");

        // TODO: some sort of argument parsing?
        // string[] file = File.ReadAllLines("C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\stop_times_trunc.txt");
        // string[] file = File.ReadAllLines("C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\stop_times.txt");
        // CSVParser parser = new CSVParser();
        Stopwatch watch = new Stopwatch();
        watch.Start();
        // parser.ParseFile(file, 16);

        // return;
        GTFSParser gtfs = new GTFSParser(rootPath, db);
        gtfs.Parse();
        // Console.WriteLine("parsed");
        gtfs.CreateTables();
        // Thread.Sleep(100);
        gtfs.UpdateTables(16);
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
    }

    private static void ShowHelp()
    {
        Console.WriteLine("tbd");
    }
}

