using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core;

public static class BlockExtensions
{
    public static ParallelTargetsRelay<TOut> BranchInParallel<TIn, TOut>(
        this IPipeRelay<TIn, TOut> relay,
        int degreeOfParallelism)
    {
        var paralleledRelay = new ParallelTargetsRelay<TOut>(degreeOfParallelism);
        relay.LinkTo(paralleledRelay);
        return paralleledRelay;
    }

    public static SequentialTargetsRelay<TOut> BranchSequential<TIn, TOut>(this IPipeRelay<TIn, TOut> relay)
    {
        var sequentialTargetsRelay = new SequentialTargetsRelay<TOut>();
        relay.LinkTo(sequentialTargetsRelay);
        return sequentialTargetsRelay;
    }

    public static MultiBlockLinkerBase<T> AddLink<T>(this MultiBlockLinkerBase<T> linker, T pipeBlock)
        where T : IPipeBlock
    {
        linker.LinkTo(pipeBlock);
        return linker;
    }

    public static IPipeRelay<TOut1, TOut2> To<TIn, TOut1, TOut2>(
        this IPipeRelay<TIn, TOut1> source,
        IPipeRelay<TOut1, TOut2> target)
    {
        source.LinkTo(target);
        return target;
    }

    public static IPipeRelay<TIn, TOut> To<TIn, TOut>(
        this IPipeRelay<TIn, TOut> source,
        IPipeTarget<TOut> target)
    {
        source.LinkTo(target);
        return source;
    }

    public static IPipeRelay<TIn, TOut> To<TIn, TOut>(
        this IPipeLinker<IPipeTarget<TIn>> linker,
        IPipeRelay<TIn, TOut> target)
    {
        linker.LinkTo(target);
        return target;
    }
}