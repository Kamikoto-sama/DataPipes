using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.Sources;

namespace DataPipes.Core.BuiltIn.Sources;

public class EnumerablePipeSource<T>(IEnumerable<T> source, bool readToEnd) : PipeSourceBase<T>, IDisposable
{
    private readonly IEnumerator<T> sourceEnumerator = source.GetEnumerator();
    private bool moveNext = true;

    public override Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken)
    {
        if (moveNext && !sourceEnumerator.MoveNext())
        {
            if (readToEnd)
                return ToTaskResult(default, true);
            var tcs = new TaskCompletionSource<IPipeSourceConsumeResult<T>>();
            cancellationToken.Register(() => tcs.TrySetCanceled());
            return tcs.Task;
        }

        moveNext = false;
        return ToTaskResult(sourceEnumerator.Current, false);
    }

    public override Task Commit(IPipeSourceConsumeResult<T> consumeResult)
    {
        EnsureResultType<EnumerableSourceConsumeResult<T>>(consumeResult);
        moveNext = true;
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        sourceEnumerator.Dispose();
    }

    private static Task<IPipeSourceConsumeResult<T>> ToTaskResult(T? source, bool endOfSource)
    {
        return Task.FromResult<IPipeSourceConsumeResult<T>>(new EnumerableSourceConsumeResult<T>(source, endOfSource));
    }
}

file record EnumerableSourceConsumeResult<T>(T? Payload, bool EndOfSource) : IPipeSourceConsumeResult<T>;