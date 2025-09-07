namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeRunner : IPipeBlock
{
    Task Run(CancellationToken cancellationToken);
}