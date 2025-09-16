using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SingleTargetRelayBase<TIn, TOut> : SingleBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>
{
    public async Task HandleEvent(TIn payload, CancellationToken cancellationToken)
    {
        await HandleEvent(payload, SingleBlock, cancellationToken);
    }

    protected abstract Task HandleEvent(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);
}