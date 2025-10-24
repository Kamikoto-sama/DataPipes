namespace DataPipes.Core.BuiltIn.Sources;

public class FilePipeSource(string filePath, bool readToEnd)
    : EnumerablePipeSource<string>(File.ReadLines(filePath), readToEnd);