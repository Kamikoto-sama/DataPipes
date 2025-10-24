using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;

namespace DataPipes.Core.Abstractions.Sources;

public abstract class PipeSourceBase<T> : IPipeSource<T>
{
    public virtual PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this);

    public virtual Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public abstract Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken);

    public abstract Task Commit(IPipeSourceConsumeResult<T> consumeResult);

    public static TResult EnsureResultType<TResult>(IPipeSourceConsumeResult<T> result)
    {
        if (result is TResult expectedResult)
            return expectedResult;
        throw new ArgumentException($"Expected result of type '{nameof(TResult)}', but got type '{result.GetType()}'");
    }
}