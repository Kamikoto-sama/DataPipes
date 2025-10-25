using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.Linkers;

namespace DataPipes.Core.Relays;

public abstract class ParallelTargetsRelayBase<TIn, TOut>(int degreeOfParallelism)
    : MultiBlockLinkerBase<IPipeTarget<TOut>>, IPipeRelay<TIn, TOut>, IDisposable
{
    private readonly SemaphoreSlim semaphore = new(degreeOfParallelism);

    //TODO: Optimize
    public async Task HandlePayload(TIn payload, CancellationToken cancellationToken)
    {
        var handleTasks = Blocks.Select<IPipeTarget<TOut>, Task>(async target =>
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                await HandlePayload(payload, target, cancellationToken);
            }
            finally
            {
                semaphore.Release();
            }
        });

        await Task.WhenAll(handleTasks);
    }

    protected abstract Task HandlePayload(TIn payload, IPipeTarget<TOut>? target, CancellationToken cancellationToken);

    public virtual void Dispose() => semaphore.Dispose();
}