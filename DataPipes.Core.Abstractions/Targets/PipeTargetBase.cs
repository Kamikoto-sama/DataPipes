using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Targets;

public abstract class PipeTargetBase<T> : IPipeTarget<T>
{
    public PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this);

    public abstract Task Initialize(CancellationToken cancellationToken);

    public abstract Task HandlePayload(T payload, CancellationToken cancellationToken);
}