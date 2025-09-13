using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions;

public static class PipeBlockMetaBuilder
{
    public static PipeBlockMeta Create(IPipeBlock block)
    {
        return Create(block, []);
    }

    public static PipeBlockMeta Create(IPipeBlock block, IReadOnlyCollection<IPipeBlock> linkedBlocks)
    {
        var type = block.GetType();
        var blockName = GetTypeGenericName(type);
        return new PipeBlockMeta(blockName) { LinkedBlocks = linkedBlocks };
    }

    private static string GetTypeGenericName(Type type)
    {
        if (!type.IsGenericType)
            return type.Name;
        var genericTypes = type.GetGenericArguments().Select(t => t.Name);
        return $"{type.Name}<{string.Join(",", genericTypes)}>";
    }
}