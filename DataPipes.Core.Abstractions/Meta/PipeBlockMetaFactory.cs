using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Meta;

public static class PipeBlockMetaFactory
{
    public static PipeBlockMeta Create(IPipeBlock block)
    {
        return Create(block, []);
    }

    public static PipeBlockMeta Create(IPipeBlock block, IEnumerable<IPipeBlock?> linkedBlocks)
    {
        var type = block.GetType();
        var blockName = GetTypeGenericName(type);
        return new PipeBlockMeta(blockName) { LinkedBlocks = linkedBlocks.Where(b => b != null).ToArray()! };
    }

    private static string GetTypeGenericName(Type type)
    {
        if (!type.IsGenericType)
            return type.Name;
        var genericTypes = type.GetGenericArguments().Select(t => t.Name);
        return $"{type.Name}<{string.Join(",", genericTypes)}>";
    }
}