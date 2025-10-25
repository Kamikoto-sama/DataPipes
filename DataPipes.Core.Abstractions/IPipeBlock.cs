namespace DataPipes.Core.Abstractions;

public interface IPipeBlock
{
    Task Initialize(CancellationToken cancellationToken);
    PipeBlockMeta Meta { get; }
}