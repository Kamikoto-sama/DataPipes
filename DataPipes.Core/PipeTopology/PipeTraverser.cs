using DataPipes.Core.Abstractions;

namespace DataPipes.Core.PipeTopology;

internal class PipeTraverser
{
    public readonly List<IPipeBlock> Blocks = [];
    public readonly List<(int Linker, int[] Linked)> BlockLinks = [];
    private readonly HashSet<IPipeBlock> pathBlocks = [];

    public int TraversePipe(IPipeBlock pipeBlock)
    {
        var blockIndex = Blocks.IndexOf(pipeBlock);
        var traversed = blockIndex > -1;
        if (!traversed)
        {
            blockIndex = Blocks.Count;
            Blocks.Add(pipeBlock);
        }

        if (!pathBlocks.Add(pipeBlock))
            ThrowCycleDetected(blockIndex);
        if (traversed)
        {
            pathBlocks.Remove(pipeBlock);
            return blockIndex;
        }

        var linkedBlockIndices = pipeBlock.Meta.LinkedBlocks.Select(TraversePipe).ToArray();
        if (linkedBlockIndices.Length > 0)
            BlockLinks.Add((blockIndex, linkedBlockIndices));

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
        var blocksInCycle = pathBlocks.Select(b => b.Meta.Name);
        return string.Join(" -> ", blocksInCycle) + " ->|";
    }
}