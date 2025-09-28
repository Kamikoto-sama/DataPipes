using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.PipeTopology;

public class DefaultPipeTopologyExplorer : IPipeTopologyExplorer
{
    private readonly List<IPipeBlock> blocks = [];
    private readonly List<PipeTopologyLink> blockLinks = [];
    private readonly HashSet<IPipeBlock> pathBlocks = [];

    public PipeTopology Explore(IPipeBlock pipeEntryBlock)
    {
        TraversePipe(pipeEntryBlock);
        blockLinks.Reverse();
        return new PipeTopology(blocks.ToArray(), blockLinks.ToArray());
    }

    private int TraversePipe(IPipeBlock pipeBlock)
    {
        var blockIndex = blocks.IndexOf(pipeBlock);
        if (blockIndex == -1)
        {
            blockIndex = blocks.Count;
            blocks.Add(pipeBlock);
        }

        if (!pathBlocks.Add(pipeBlock))
            ThrowCycleDetected(blockIndex);

        var linkedBlockIndices = pipeBlock.Meta.LinkedBlocks.Select(TraversePipe).ToArray();
        if (linkedBlockIndices.Length > 0)
            blockLinks.Add(new PipeTopologyLink(blockIndex, linkedBlockIndices));

        pathBlocks.Remove(pipeBlock);
        return blockIndex;
    }

    private void ThrowCycleDetected(int blockIndex)
    {
        var cyclePath = BuildCyclePath(blockIndex);
        var errorMessage = $"Cycled link detected: '{cyclePath}'";
        throw new PipeTopologyException(errorMessage);
    }

    private string BuildCyclePath(int blockIndex)
    {
        var blocksInCycle = blocks.Skip(blockIndex).Select(b => b.Meta.Name);
        return string.Join(" -> ", blocksInCycle) + " -|";
    }
}