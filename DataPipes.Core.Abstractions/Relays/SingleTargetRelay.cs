using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SingleTargetRelay<TIn, TOut> : IPipeRelay<TIn, TOut>
{
    private IPipeTarget<TOut>? singleTarget;

    public abstract Task Initialize();

    public void LinkTo(IPipeTarget<TOut> target)
    {
        if (singleTarget != null)
            throw new InvalidOperationException($"Target '{target}' already linked");
        singleTarget = target;
    }

    public async Task HandleEvent(TIn payload) => await HandleEvent(payload, singleTarget);

    protected abstract Task HandleEvent(TIn payload, IPipeTarget<TOut>? target);
}