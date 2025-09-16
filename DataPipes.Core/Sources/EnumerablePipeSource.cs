using DataPipes.Core.Abstractions;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;

namespace DataPipes.Core.Sources;

public class EnumerablePipeSource<T>(IEnumerable<T> source) : PipeSourceBase<T>, IDisposable
{
    private readonly IEnumerator<T> sourceEnumerator = source.GetEnumerator();
    private bool moveNext = true;

    public override Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken)
    {
        if (moveNext && !sourceEnumerator.MoveNext())
        {
            var tcs = new TaskCompletionSource<IPipeSourceConsumeResult<T>>();
            cancellationToken.Register(() => tcs.TrySetCanceled());
            return tcs.Task;
        }

        moveNext = false;
        var result = new EnumerableSourceConsumeResult<T>(sourceEnumerator.Current);
        return Task.FromResult<IPipeSourceConsumeResult<T>>(result);
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
}

file record EnumerableSourceConsumeResult<T>(T Payload, bool EndOfSource = false) : IPipeSourceConsumeResult<T>;