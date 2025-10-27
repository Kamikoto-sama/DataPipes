namespace DataPipes.Core.EdgeBlocks.Sources;

public class FilePipeSource(string filePath, bool readToEnd)
    : EnumerablePipeSource<string>(File.ReadLines(filePath), readToEnd);