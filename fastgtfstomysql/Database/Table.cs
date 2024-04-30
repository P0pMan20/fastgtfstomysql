namespace FastGTFSImport;

// Table to be added
public class Table
{
    public Table(string name, ValueTuple<string, string>[] ColumnNamesAndTheirCorrespondingDatatype)
    {
        Name = name;
        this.ColumnNamesAndTheirCorrespondingDatatype = ColumnNamesAndTheirCorrespondingDatatype;
    }
    public string Name;
    // using valueTuple as prior to assignment I want it to be mutable
    public ValueTuple<string, string>[] ColumnNamesAndTheirCorrespondingDatatype;
    
}