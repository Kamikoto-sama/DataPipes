using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class MultiTargetRelay<TIn, TOut> : IPipeRelay<TIn, TOut>
{
    public IReadOnlyCollection<IPipeTarget<TOut>> LinkedTargets => Targets;
    public virtual PipeBlockMeta Meta => PipeBlockMetaBuilder.Create(this, Targets);

    protected readonly List<IPipeTarget<TOut>> Targets = [];

    public abstract Task Initialize(CancellationToken cancellationToken);

    public void LinkTo(IPipeTarget<TOut> target) => Targets.Add(target);

    public abstract Task HandleEvent(TIn pipeEvent, CancellationToken cancellationToken);
}