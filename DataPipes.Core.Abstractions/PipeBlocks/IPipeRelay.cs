namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeRelay<TIn, TOut> : IPipeTarget<TIn>
{
    void LinkTo(IPipeTarget<TOut> target);
}