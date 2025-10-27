using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Linkers;

namespace DataPipes.Core.Readers;

//TODO: MultiSourceReader
public abstract class SingleSourceReaderBase<TIn, TOut>
    : SingleBlockLinkerBase<IPipeSource<TIn>>, IPipeReader<TIn, TOut>
{
    public async Task<IPipeSourceConsumeResult<TOut>> Consume(CancellationToken cancellationToken)
    {
        if (SingleBlock == null)
            throw new InvalidOperationException("No source was linked");
        return await Consume(SingleBlock, cancellationToken);
    }

    protected abstract Task<IPipeSourceConsumeResult<TOut>> Consume(
        IPipeSource<TIn> pipeSource,
        CancellationToken cancellationToken);

    public async Task Commit(IPipeSourceConsumeResult<TOut> consumeResult)
    {
        if (SingleBlock == null)
            throw new InvalidOperationException("No source was linked");
        await Commit(SingleBlock, consumeResult);
    }

    protected abstract Task Commit(IPipeSource<TIn> pipeSource, IPipeSourceConsumeResult<TOut> consumeResult);
}