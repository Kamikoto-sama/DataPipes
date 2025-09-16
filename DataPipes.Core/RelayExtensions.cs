using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core;

public static class RelayExtensions
{
    public static ParallelTargetsRelay<TOut> AsParallelBranch<TIn, TOut>(
        this IPipeRelay<TIn, TOut> relay,
        int degreeOfParallelism)
    {
        var paralleledRelay = new ParallelTargetsRelay<TOut>(degreeOfParallelism);
        relay.LinkTo(paralleledRelay);
        return paralleledRelay;
    }

    public static IPipeRelay<TOut1, TOut2> To<TIn, TOut1, TOut2>(
        this IPipeRelay<TIn, TOut1> source,
        IPipeRelay<TOut1, TOut2> target)
    {
        source.LinkTo(target);
        return target;
    }

    public static IPipeTarget<TOut> To<TIn, TOut>(
        this IPipeRelay<TIn, TOut> source,
        IPipeTarget<TOut> target)
    {
        source.LinkTo(target);
        return target;
    }
}