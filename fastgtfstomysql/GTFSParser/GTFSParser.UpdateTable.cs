namespace FastGTFSImport;

public partial class GTFSParser
{
    private CountdownEvent _countdownEvent;
    private string[][] formattedFile;
    private void UpdateTable(Table table, int numberOfThreads)
    {
        CSVParser parser = new CSVParser();
        formattedFile = parser.ParseFile(File.ReadAllLines(Path.Join(_rootPath, table.Name + ".txt")), numberOfThreads).Item2;
        // Console.WriteLine($"Read {table.Name}");
        if (formattedFile.Length < numberOfThreads)
        {
            numberOfThreads = 1;
        }
        int numberOfLinesPerThread = (int) MathF.Floor((formattedFile.Length) / numberOfThreads);
        Thread[] threads = new Thread[numberOfThreads];
        _countdownEvent = new CountdownEvent(numberOfThreads);
        // Console.WriteLine(numberOfLinesPerThread);
        for (int i = 0; i < numberOfThreads; i++)
        {
            //special case for the last thread
            if (i == numberOfThreads - 1)
            {
                // Console.WriteLine("last");
                threads[i] = new Thread(InsertGTFSThreadMethod);
                threads[i].Name = "GTFS-" + i;
                threads[i].Start((new Range(0, formattedFile.Length - numberOfLinesPerThread * i), table.Name));

            }
            else
            {
                threads[i] = new Thread(InsertGTFSThreadMethod);
                threads[i].Start((new Range(formattedFile.Length - numberOfLinesPerThread * (i + 1),
                    formattedFile.Length - numberOfLinesPerThread * i), table.Name));
                threads[i].Name = "GTFS-" + i;
            }
        }

        _countdownEvent.Wait();

    }

    private void InsertGTFSThreadMethod(object container)
    {
        IDatabase _database = _databaseFactory.Produce();
        Range range =  ((ValueTuple<Range,string>) container ).Item1;
        string name = ((ValueTuple<Range,string>) container ).Item2;
        // Console.WriteLine("hi");
        // Console.WriteLine(range);
        _database.BeginTransaction();
        for (int j = range.Start.Value; j < range.End.Value; j++)
        {
            _database.InsertRow(name, formattedFile[j]);

            // MySqlCommand command = new MySqlCommand(
            //     $"INSERT INTO `stop_times` (trip_id, arrival_time, departure_time, stop_id, stop_sequence, pickup_type, drop_off_type) VALUES ('{res.Item2[j][0]}', '{res.Item2[j][1]}', '{res.Item2[j][2]}', '{res.Item2[j][3]}', '{res.Item2[j][4]}', '{res.Item2[j][5]}', '{res.Item2[j][6]}')",
            //     dbConnection, trans);
            // command.ExecuteNonQuery();


            // Util.PrintElements(ParseLine(_fileToParse[i]));

        }
        _database.CommitTransaction();
        _countdownEvent.Signal();
    }
}