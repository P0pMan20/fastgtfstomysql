namespace FastGTFSImport;

// Table to be added
public class Table
{
    public Table(string name, ValueTuple<string, string>[] ColumnNamesAndTheirCorrespondingDatatype, string[] primaryKeys)
    {
        Name = name;
        this.ColumnNamesAndTheirCorrespondingDatatype = ColumnNamesAndTheirCorrespondingDatatype;
        this.PrimaryKeys = primaryKeys;
    }
    public string Name;

    public string[] PrimaryKeys;
    // using valueTuple as prior to assignment I want it to be mutable
    public ValueTuple<string, string>[] ColumnNamesAndTheirCorrespondingDatatype;
    
}