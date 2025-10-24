using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Pipelines.Blocks;

public class ErrorHandlingBlock<T> : PipelineRelayBase<T, T>
{
    protected override Task HandlePayload(
        PipelinePayload<T> payload,
        IPipeTarget<PipelinePayload<T>>? target,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}