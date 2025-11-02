using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Pipelines.Abstractions;

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