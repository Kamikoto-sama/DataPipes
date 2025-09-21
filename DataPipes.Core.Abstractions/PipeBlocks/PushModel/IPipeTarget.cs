namespace DataPipes.Core.Abstractions.PipeBlocks.PushModel;

public interface IPipeTarget<in T> : IPipeBlock
{
    Task HandlePayload(T payload, CancellationToken cancellationToken);
}