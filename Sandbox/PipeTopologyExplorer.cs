using DataPipes.Core.Abstractions.PipeBlocks;

namespace Sandbox;

public class PipeTopologyExplorer
{
    private readonly List<IPipeBlock> blocks = [];
    private readonly List<PipeTopologyLink> blockLinks = [];

    public PipeTopology Explore(IPipeBlock entryBlock)
    {
        ExploreInternal(entryBlock);
        blockLinks.Reverse();
        return new PipeTopology(blocks.ToArray(), blockLinks.ToArray());
    }

    private int ExploreInternal(IPipeBlock entryBlock)
    {
        var blockIndex = blocks.Count;
        blocks.Add(entryBlock);
        var linkedBlockIndices = entryBlock.Meta.LinkedBlocks.Select(ExploreInternal).ToArray();

        if (linkedBlockIndices.Length > 0)
            blockLinks.Add(new PipeTopologyLink(blockIndex, linkedBlockIndices));
        return blockIndex;
    }
}

public record PipeTopology(IPipeBlock[] Blocks, PipeTopologyLink[] BlockLinks);

public record PipeTopologyLink(int LinkerIndex, int[] LinkedBlockIndices);