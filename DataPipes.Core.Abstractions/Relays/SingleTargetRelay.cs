using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class SingleTargetRelay<TIn, TOut> : IPipeRelay<TIn, TOut>
{
    public IReadOnlyCollection<IPipeTarget<TOut>> LinkedTargets => singleTarget == null ? [] : [singleTarget];
    public virtual PipeBlockMeta Meta => PipeBlockMetaBuilder.Create(this, LinkedTargets);

    private IPipeTarget<TOut>? singleTarget;

    public abstract Task Initialize(CancellationToken cancellationToken);

    public void LinkTo(IPipeTarget<TOut> target)
    {
        if (singleTarget != null)
            throw new InvalidOperationException($"Relay is already linked to target '{target}'");
        singleTarget = target;
    }

    public async Task HandleEvent(TIn payload, CancellationToken cancellationToken)
    {
        await HandleEvent(payload, singleTarget, cancellationToken);
    }

    protected abstract Task HandleEvent(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);
}