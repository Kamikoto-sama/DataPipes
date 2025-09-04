namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeSourceConsumeResult<T>
{
    T Payload { get; }
}