using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class ParallelTargetsRelayBase<TIn, TOut>(int degreeOfParallelism)
    : MultiBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>, IDisposable
{
    private readonly SemaphoreSlim semaphore = new(degreeOfParallelism);

    //TODO: Optimize
    public async Task HandleEvent(TIn payload, CancellationToken cancellationToken)
    {
        var handleTasks = Blocks.Select<IPipeTarget<TOut>, Task>(async target =>
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                await HandleEvent(payload, target, cancellationToken);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(handleTasks);
    }

    protected abstract Task HandleEvent(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);

    public virtual void Dispose() => semaphore.Dispose();
}