using DataPipes.Core.Abstractions;

namespace DataPipes.Pipelines.Abstractions;

public interface IPipeline
{
    PipelineContext Context { get; }
    IReadOnlyList<IPipeRunner> EntryBlocks { get; }
    Task Initialize(CancellationToken cancellationToken);
    Task Run(CancellationToken cancellationToken);
}