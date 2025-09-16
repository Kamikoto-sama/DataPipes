namespace DataPipes.Core.Abstractions.PipeBlocks.PushModel;

public interface IPipeTarget<in T> : IPipeBlock
{
    Task HandleEvent(T payload, CancellationToken cancellationToken);
}