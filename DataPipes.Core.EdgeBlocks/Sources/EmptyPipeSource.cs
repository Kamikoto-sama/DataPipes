using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Sources;

namespace DataPipes.Core.EdgeBlocks.Sources;

public class EmptyPipeSource<T>(bool readToEnd) : PipeSourceBase<T>
{
    public override Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken)
    {
        if (readToEnd)
            return Task.FromResult<IPipeSourceConsumeResult<T>>(new EmptyPipeSourceConsumeResult(default, true));

        var cts = new TaskCompletionSource<IPipeSourceConsumeResult<T>>();
        cancellationToken.Register(() => cts.TrySetCanceled());
        return cts.Task;
    }

    public override Task Commit(IPipeSourceConsumeResult<T> consumeResult)
    {
        return Task.CompletedTask;
    }

    private record EmptyPipeSourceConsumeResult(T? Payload, bool EndOfSource) : IPipeSourceConsumeResult<T>;
}