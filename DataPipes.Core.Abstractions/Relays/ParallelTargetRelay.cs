using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Relays;

public abstract class ParallelTargetRelay<TIn, TOut>(int degreeOfParallelism) : MultiTargetRelay<TIn, TOut>, IDisposable
{
    private readonly SemaphoreSlim semaphore = new(degreeOfParallelism);

    public override async Task HandleEvent(TIn payload, CancellationToken cancellationToken)
    {
        var handleTasks = Targets.Select<IPipeTarget<TOut>, Task>(async target =>
        {
            await semaphore.WaitAsync();
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