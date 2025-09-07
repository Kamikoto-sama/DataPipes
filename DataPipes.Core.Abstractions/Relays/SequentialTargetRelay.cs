using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SequentialTargetRelay<TIn, TOut> : MultiTargetRelay<TIn, TOut>
{
    public override async Task HandleEvent(TIn payload, CancellationToken cancellationToken)
    {
        foreach (var target in Targets)
            await HandleEvent(payload, target);
    }

    protected abstract Task HandleEvent(TIn payload, IPipeTarget<TOut>? target);
}