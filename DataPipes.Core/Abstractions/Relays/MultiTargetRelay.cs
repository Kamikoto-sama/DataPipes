using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class MultiTargetRelay<TIn, TOut> : IPipeRelay<TIn, TOut>
{
    protected readonly List<IPipeTarget<TOut>> Targets = [];

    public abstract void Initialize();

    public void LinkTo(IPipeTarget<TOut> target) => Targets.Add(target);

    public abstract Task HandleEvent(TIn pipeEvent);
}