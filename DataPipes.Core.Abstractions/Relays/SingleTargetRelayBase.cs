using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SingleTargetRelayBase<TIn, TOut> : SingleBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>
{
    public async Task HandlePayload(TIn payload, CancellationToken cancellationToken)
    {
        await HandlePayload(payload, SingleBlock, cancellationToken);
    }

    protected abstract Task HandlePayload(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);
}