using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Targets;

public abstract class PipeTargetBase<T> : IPipeTarget<T>
{
    public PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this);

    public virtual Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public abstract Task HandlePayload(T payload, CancellationToken cancellationToken);
}