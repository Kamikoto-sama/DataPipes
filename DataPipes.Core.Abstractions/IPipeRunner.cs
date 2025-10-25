namespace DataPipes.Core.Abstractions;

public interface IPipeRunner : IPipeBlock
{
    Task Run(CancellationToken cancellationToken);
}