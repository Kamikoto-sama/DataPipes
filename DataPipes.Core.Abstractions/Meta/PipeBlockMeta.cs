using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Meta;

public record PipeBlockMeta(string Name)
{
    public IReadOnlyCollection<IPipeBlock> LinkedBlocks { get; init; } = [];
}