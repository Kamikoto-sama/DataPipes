﻿using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;

namespace DataPipes.Core.Abstractions;

public abstract class PipeSourceBase<T> : IPipeSource<T>
{
    public virtual PipeBlockMeta Meta => PipeBlockMetaBuilder.Create(this);

    public abstract Task Initialize(CancellationToken cancellationToken);

    public abstract Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken);

    public abstract Task Commit(IPipeSourceConsumeResult<T> consumeResult);

    protected TResult EnsureResultType<TResult>(IPipeSourceConsumeResult<T> result)
    {
        if (result is TResult expectedResult)
            return expectedResult;
        throw new ArgumentException($"Expected result of type '{nameof(TResult)}', but got type '{result.GetType()}'");
    }
}