namespace DataPipes.Core.PipeTopology;

public record PipeTopologyLink(int LinkerIndex, IReadOnlyList<int> LinkedBlockIndices);