namespace DataPipes.Core.Abstractions;

public record PipeBlockMeta(string Name)
{
    public IReadOnlyCollection<IPipeBlock> LinkedBlocks { get; init; } = [];
}