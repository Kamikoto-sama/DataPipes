using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions;

public static class PipeBlockMetaBuilder
{
    public static PipeBlockMeta Create(IPipeBlock block)
    {
        var type = block.GetType();
        var blockName = type.FullName ?? type.Name;
        return new PipeBlockMeta(blockName);
    }
}