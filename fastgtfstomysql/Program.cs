using System.Diagnostics;

namespace FastGTFSImport;

class Program
{
    // Scan for files -> Create table headings -> Create tables if desired -> parse whole file -> insert (threaded)
    static void Main(string[] args)
    {
        string rootPath = "C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\CNS_GTFS";
        // some sort of argument parsing?
        // string[] file = File.ReadAllLines("C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\stop_times_trunc.txt");
        // string[] file = File.ReadAllLines("C:\\Users\\User\\Documents\\Development\\C#\\fastgtfstomysql\\stop_times.txt");
        // CSVParser parser = new CSVParser();
        // Stopwatch watch = new Stopwatch();
        // parser.ParseFile(file, 16);
        GTFSParser gtfs = new GTFSParser(rootPath);
        gtfs.Parse();

    }

    private static void ShowHelp()
    {
        Console.WriteLine("tbd");
    }
}

