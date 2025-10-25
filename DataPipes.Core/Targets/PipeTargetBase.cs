using DataPipes.Core.Abstractions;
using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.Meta;

namespace DataPipes.Core.Targets;

public abstract class PipeTargetBase<T> : IPipeTarget<T>
{
    public PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this);

    public virtual Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public abstract Task HandlePayload(T payload, CancellationToken cancellationToken);
}