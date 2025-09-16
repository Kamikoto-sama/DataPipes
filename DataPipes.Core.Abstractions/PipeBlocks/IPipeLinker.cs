namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeLinker<T> : IPipeBlock where T : IPipeBlock
{
    IReadOnlyCollection<T> LinkedBlocks { get; }
    void LinkTo(T pipeBlock);
    void Unlink(T pipeBlock);
    void UnlinkAll();
}