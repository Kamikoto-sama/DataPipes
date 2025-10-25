namespace DataPipes.Core.Blocks.Sources;

public class FilePipeSource(string filePath, bool readToEnd)
    : EnumerablePipeSource<string>(File.ReadLines(filePath), readToEnd);