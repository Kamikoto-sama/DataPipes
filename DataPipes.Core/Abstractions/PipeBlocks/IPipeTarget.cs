namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeTarget<T> : IPipeBlock
{
    Task HandleEvent(T payload);
}