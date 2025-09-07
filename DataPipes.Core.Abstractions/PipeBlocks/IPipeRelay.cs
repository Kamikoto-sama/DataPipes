namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeRelay<TIn, TOut> : IPipeTarget<TIn>
{
    IReadOnlyCollection<IPipeTarget<TOut>> LinkedTargets { get; }
    void LinkTo(IPipeTarget<TOut> target);
}