using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Sources;

namespace DataPipes.Core.EdgeBlocks.Sources;

public class EnumerablePipeSource<T>(IEnumerable<T> source, bool readToEnd) : PipeSourceBase<T>, IDisposable
{
    private readonly IEnumerator<T> sourceEnumerator = source.GetEnumerator();
    private bool committed = true;

    public override Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken)
    {
        if (committed && !sourceEnumerator.MoveNext())
        {
            if (readToEnd)
                return ToTaskResult(default, true);
            var tcs = new TaskCompletionSource<IPipeSourceConsumeResult<T>>();
            cancellationToken.Register(() => tcs.TrySetCanceled());
            return tcs.Task;
        }

        committed = false;
        return ToTaskResult(sourceEnumerator.Current, false);
    }

    public override Task Commit(IPipeSourceConsumeResult<T> consumeResult)
    {
        EnsureResultType<ConsumeResult>(consumeResult);
        committed = true;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        sourceEnumerator.Dispose();
    }

    private static Task<IPipeSourceConsumeResult<T>> ToTaskResult(T? source, bool endOfSource)
    {
        return Task.FromResult<IPipeSourceConsumeResult<T>>(new ConsumeResult(source, endOfSource));
    }

    private record ConsumeResult(T? Payload, bool EndOfSource) : IPipeSourceConsumeResult<T>;
}