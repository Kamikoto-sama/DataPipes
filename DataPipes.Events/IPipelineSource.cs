using DataPipes.Core.Abstractions.PipeBlocks.PullModel;

namespace DataPipes.Events;

public interface IPipelineSource<T> : IPipeSource<PipelineSourceEvent<T>>
{
    string SourceName { get; }
}