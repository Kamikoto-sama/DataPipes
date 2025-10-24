using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.PipeTopology;

public class PipeTopology
{
    public required IReadOnlyList<int> EntryBlockIndices { get; init; }
    public required IReadOnlyList<IPipeBlock> Blocks { get; init; }
    public required IReadOnlyDictionary<int, PipeTopologyLink> BlockLinks { get; init; }
}