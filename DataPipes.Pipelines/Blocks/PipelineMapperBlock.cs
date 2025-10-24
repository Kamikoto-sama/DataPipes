using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Pipelines.Blocks;

public class PipelineMapperBlock<TIn, TOut>(Func<PipelinePayload<TIn>, Task<PipelinePayload<TOut>>> mapper)
    : PipelineRelayBase<TIn, TOut>
{
    protected override async Task HandlePayload(
        PipelinePayload<TIn> payload,
        IPipeTarget<PipelinePayload<TOut>>? target,
        CancellationToken cancellationToken)
    {
        var newPayload = await mapper(payload);
        if (target != null)
            await target.HandlePayload(newPayload, cancellationToken);
    }
}