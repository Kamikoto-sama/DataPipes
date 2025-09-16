using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SequentialTargetsRelayBase<TIn, TOut>
    : MultiBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>
{
    public async Task HandleEvent(TIn payload, CancellationToken cancellationToken)
    {
        foreach (var target in Blocks)
            await HandleEvent(payload, target);
    }

    protected abstract Task HandleEvent(TIn payload, IPipeTarget<TOut>? target);
}