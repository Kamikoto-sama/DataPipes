using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Targets;

public abstract class PipeTargetBase<T> : IPipeTarget<T>
{
    public PipeBlockMeta Meta => PipeBlockMetaBuilder.Create(this);

    public abstract Task Initialize(CancellationToken cancellationToken);

    public abstract Task HandleEvent(T payload, CancellationToken cancellationToken);
}