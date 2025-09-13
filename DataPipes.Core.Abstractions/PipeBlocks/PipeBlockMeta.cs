namespace DataPipes.Core.Abstractions.PipeBlocks;

public class PipeBlockMeta
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public IReadOnlyCollection<IPipeBlock> LinkedBlocks { get; init; } = [];

    public PipeBlockMeta(string name)
    {
        Name = name;
    }
}