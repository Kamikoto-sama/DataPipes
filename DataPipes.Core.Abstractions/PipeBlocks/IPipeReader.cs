namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeReader<TIn, TOut> : IPipeSource<TOut>
{
    void ReadFrom(IPipeSource<TIn> source);
}