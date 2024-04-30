using System.Text;

namespace FastGTFSImport;

public static class Util
{
    public static void PrintElements<T>(IEnumerable<T> collection, string sep="")
    {
        foreach (var element in collection)
        {
            Console.Write(element + sep);
        }
    }
    
    public static void PrintNestedElements(string[][] collection)
    {
        for (int i = 0; i < collection.Length; i++)
        {
            for (int j = 0; j < collection[i].Length; j++)
            {
                Console.Write(i + " " + j);
                Console.Write(collection[i][j]);
            }
        }
    }

    public static string FlattenToText(this string[] arr)
    {
        // assumption
        StringBuilder sb = new StringBuilder(arr.Length+arr[0].Length*arr.Length);
        foreach (string str in arr)
        {
            sb.Append("\"");
            sb.Append(str);
            sb.Append("\",");
        }

        sb.Remove(sb.Length - 1, 1);
        return sb.ToString();

    }
}