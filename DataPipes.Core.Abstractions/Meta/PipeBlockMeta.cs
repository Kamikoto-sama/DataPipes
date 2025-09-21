using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Meta;

public class PipeBlockMeta(string name)
{
    public string Name { get; init; } = name;
    public string? Description { get; init; }
    public IReadOnlyCollection<IPipeBlock> LinkedBlocks { get; init; } = [];
}