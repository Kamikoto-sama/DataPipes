using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.Linkers;

namespace DataPipes.Core.Relays;

public abstract class SequentialTargetsRelayBase<TIn, TOut>
    : MultiBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>
{
    public async Task HandlePayload(TIn payload, CancellationToken cancellationToken)
    {
        foreach (var target in Blocks)
            await HandlePayload(payload, target, cancellationToken);
    }

    protected abstract Task HandlePayload(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);
}