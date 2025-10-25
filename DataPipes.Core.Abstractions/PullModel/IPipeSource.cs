namespace DataPipes.Core.Abstractions.PullModel;

public interface IPipeSource<T> : IPipeBlock
{
    Task<IPipeSourceConsumeResult<T>> Consume(CancellationToken cancellationToken);
    Task Commit(IPipeSourceConsumeResult<T> consumeResult);
}