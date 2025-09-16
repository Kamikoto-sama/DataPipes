using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions;

public abstract class PipeTargetBase<T> : IPipeTarget<T>
{
    public PipeBlockMeta Meta => PipeBlockMetaBuilder.Create(this);

    public abstract Task Initialize(CancellationToken cancellationToken);

    public abstract Task HandleEvent(T payload, CancellationToken cancellationToken);
}