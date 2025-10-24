using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.PipeTopology;

public class DefaultPipeTopologyExplorer : IPipeTopologyExplorer
{
    public PipeTopology Explore(params IPipeBlock[] pipeEntryBlocks)
    {
        var pipeTraverser = new DefaultPipeTraverser();
        var entryBlockIndices = new List<int>();
        foreach (var pipeEntryBlock in pipeEntryBlocks)
        {
            entryBlockIndices.Add(pipeTraverser.Blocks.Count);
            pipeTraverser.TraversePipe(pipeEntryBlock);
        }

        var links = pipeTraverser.BlockLinks.ToDictionary(l => l.Linker, l => new PipeTopologyLink(l.Linker, l.Linked));
        return new PipeTopology
        {
            EntryBlockIndices = entryBlockIndices.ToArray(),
            Blocks = pipeTraverser.Blocks,
            BlockLinks = links
        };
    }
}