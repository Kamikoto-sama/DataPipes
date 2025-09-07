namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeTarget<in T> : IPipeBlock
{
    Task HandleEvent(T payload, CancellationToken cancellationToken);
}