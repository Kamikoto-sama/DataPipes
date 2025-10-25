using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.Linkers;

namespace DataPipes.Core.Relays;

public abstract class SingleTargetRelayBase<TIn, TOut> : SingleBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>
{
    public async Task HandlePayload(TIn payload, CancellationToken cancellationToken)
    {
        await HandlePayload(payload, SingleBlock, cancellationToken);
    }

    protected abstract Task HandlePayload(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);
}