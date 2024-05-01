using System.Text;

namespace FastGTFSImport;

public class CSVParser
{
    private CountdownEvent _countdownEvent;
    // race conditions from multiple threads accessing the same array aren't an issue
    // as they're each writing to their own section
    private string[][] _parsedData;
    private string[] CSVFile;
    // TODO: I don't think I need to have a tuple here anymore, I can just emit a jagged array?
    public Tuple<string[], string[][]> ParseFile(string[] fileToParse, int numberOfThreads)
    {
        CSVFile = fileToParse;
        string[] columns = ParseLine(CSVFile[0]);
        // exclude "columns"
        _parsedData = new string[CSVFile.Length-1][];
        int numberOfLinesPerThread = (int) MathF.Floor((CSVFile.Length - 1) / numberOfThreads);
        Thread[] threads = new Thread[numberOfThreads];
        _countdownEvent = new CountdownEvent(numberOfThreads);

        for (int i = 0; i < numberOfThreads; i++)
        {
            //special case for the last thread
            if (i == numberOfThreads - 1)
            {
                threads[i] = new Thread(CSVThreadMethod);
                threads[i].Name = "CSV-" + i;
                threads[i].Start(new Range(1, CSVFile.Length - numberOfLinesPerThread * i));

            }
            else
            {
                threads[i] = new Thread(CSVThreadMethod);
                threads[i].Start(new Range(CSVFile.Length - numberOfLinesPerThread * (i + 1),
                    CSVFile.Length - numberOfLinesPerThread * i));
                threads[i].Name = "CSV-" + i;
            }
        }

        _countdownEvent.Wait();
        return new Tuple<string[], string[][]>(columns, _parsedData);
    }

    private void CSVThreadMethod(object container)
    {
        Range range = (Range) container;
        // Console.WriteLine($"Thread {Thread.CurrentThread.Name} working on range {range.Start}-{range.End}");
        for (int i = range.Start.Value; i < range.End.Value; i++)
        {
            _parsedData[i-1] = ParseLine(CSVFile[i]);


            // Util.PrintElements(ParseLine(_fileToParse[i]));

        }

        _countdownEvent.Signal();
    }

    public static string[] ParseColumns(string[] file)
    {
        return ParseLine(file[0]);
    }
    public static string[] ParseColumns(string filePath)
    {
        return ParseLine(File.ReadLines(filePath).First());
    }
    // this is my own shitty method, probably can and **should** be optimised/Replaced, Rider complains about all the allocations!
    private static string[] ParseLine(string line) 
    {
    
        // TODO: attempt to fix broken thing
        // this escapes quotes so they can be properly inserted
        StringBuilder sb = new StringBuilder(line);
        sb.Replace("\"", "\\\"");
        line = sb.ToString();
        
        
        int indentationLevel = 0;
        List<int> indicesOfSeperators = new List<int>();
        char previouschar = '0';
        for (int i = 0; i < line.Length; i++)
        {
            if (i > 0)
            {
                previouschar = line[i - 1];

            }

            if (line[i] == ',' && indentationLevel > 1 && previouschar == '"')
            {
                indentationLevel = 0;
            }

            if (line[i] == ',' && indentationLevel > 1)
            {
                indentationLevel -= 1;
            }

            if (line[i] == '"')
            {
                indentationLevel += 1;
            }

            // ind[i] = indentation;
            if (indentationLevel == 0 && line[i] == ',')
            {
                indicesOfSeperators.Add(i);
            }

        }


        int previous = 0;
        string[] output = new string[indicesOfSeperators.Count+1];
        
        for (int i = 0; i < indicesOfSeperators.Count; i++)
        {
            if (previous == 0)
            {
                output[i]=line.Substring(previous, indicesOfSeperators[i]);
            }
            else
            {
                // +1 and -1 remove trailing and preceding commas
                output[i]=line.Substring(previous + 1, indicesOfSeperators[i] - previous - 1);


            }

            previous = indicesOfSeperators[i];
        }

        output[indicesOfSeperators.Count] =
            line.Substring(indicesOfSeperators[indicesOfSeperators.Count - 1] + 1, line.Length-indicesOfSeperators[indicesOfSeperators.Count - 1] - 1);

        return output;
    }
}