using System.Threading.Channels;
using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.Sources;

namespace DataPipes.Pipelines.Linearizing;

public class LocalLinearizer<T> : PipeSourceBase<T>, IPipeTarget<T>, IPipeSource<T>
{
    private readonly Channel<QueueItem> queue = Channel.CreateUnbounded<QueueItem>();

    public void Complete()
    {
        queue.Writer.TryComplete();
    }

    public async Task HandlePayload(T payload, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource();
        cancellationToken.Register(() => tcs.TrySetCanceled());
        await queue.Writer.WriteAsync(new QueueItem(payload, tcs), cancellationToken);
        await tcs.Task;
    }

    public override async Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken)
    {
        var reader = queue.Reader;
        var completed = !await reader.WaitToReadAsync(cancellationToken);
        if (reader.TryPeek(out var item))
            return item;
        return completed
            ? new QueueItem(default, null, true)
            : throw new InvalidOperationException("Failed to peek item");
    }

    public override Task Commit(IPipeSourceConsumeResult<T> consumeResult)
    {
        var item = EnsureResultType<QueueItem>(consumeResult, true);
        item.Tcs!.TrySetResult();
        return !queue.Reader.TryRead(out _)
            ? throw new InvalidOperationException("Failed to commit item")
            : Task.CompletedTask;
    }

    private record QueueItem(T? Payload, TaskCompletionSource? Tcs, bool EndOfSource = false)
        : IPipeSourceConsumeResult<T>;
}