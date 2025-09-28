using System.Threading.Channels;
using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.Sources;

namespace DataPipes.Events;

public class LocalLinearizer<T> : ILinearizer<T>
{
    public PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this);

    private readonly Channel<QueueItem> queue = Channel.CreateUnbounded<QueueItem>();

    public Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task HandlePayload(T payload, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        cancellationToken.Register(() => tcs.TrySetCanceled());
        await queue.Writer.WriteAsync(new QueueItem(payload, tcs), cancellationToken);
        await tcs.Task;
    }

    public async Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken)
    {
        var reader = queue.Reader;
        await reader.WaitToReadAsync(cancellationToken);
        return reader.TryPeek(out var item) ? item : throw new InvalidOperationException("Failed to peek item");
    }

    public Task Commit(IPipeSourceConsumeResult<T> consumeResult)
    {
        var item = PipeSourceBase<T>.EnsureResultType<QueueItem>(consumeResult);
        item.Tcs.TrySetResult();
        return Task.CompletedTask;
    }

    private record QueueItem(T Payload, TaskCompletionSource Tcs, bool EndOfSource = false)
        : IPipeSourceConsumeResult<T>;
}