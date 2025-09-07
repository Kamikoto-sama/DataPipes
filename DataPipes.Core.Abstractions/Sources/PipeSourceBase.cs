using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Sources;

public abstract class PipeSourceBase<T> : IPipeSource<T>
{
    public virtual PipeBlockMeta Meta => PipeBlockMetaBuilder.Create(this);

    public abstract Task Initialize(CancellationToken cancellationToken);

    public abstract Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken);

    public abstract Task Commit(IPipeSourceConsumeResult<T> consumeResult);
}