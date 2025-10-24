using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Pipelines.Blocks;

public class PipelineRelay<T> : PipelineRelayBase<T, T>
{
    protected override async Task HandlePayload(PipelinePayload<T> payload, IPipeTarget<PipelinePayload<T>>? target,
        CancellationToken cancellationToken)
    {
        if (target != null)
            await target.HandlePayload(payload, cancellationToken);
    }
}