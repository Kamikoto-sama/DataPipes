using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SingleTargetRelay<TIn, TOut> : IPipeRelay<TIn, TOut>
{
    private IPipeTarget<TOut>? singleTarget;

    public abstract void Initialize();

    public void LinkTo(IPipeTarget<TOut> target)
    {
        if (singleTarget != null)
            throw new InvalidOperationException("Target already linked");
        singleTarget = target;
    }

    public async Task HandleEvent(TIn pipeEvent) => await HandleEvent(pipeEvent, singleTarget);

    protected abstract Task HandleEvent(TIn pipeEvent, IPipeTarget<TOut>? target);
}