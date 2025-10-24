using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Blocks;

namespace DataPipes.Core;

public static class BlockExtensions
{
    public static ParallelTargetsRelay<T> BranchInParallel<T>(
        this IPipeTargetLinker<T> relay,
        int degreeOfParallelism)
    {
        var paralleledRelay = new ParallelTargetsRelay<T>(degreeOfParallelism);
        relay.LinkTo(paralleledRelay);
        return paralleledRelay;
    }

    public static SequentialTargetsRelay<T> BranchSequentially<T>(this IPipeTargetLinker<T> relay)
    {
        var sequentialTargetsRelay = new SequentialTargetsRelay<T>();
        relay.LinkTo(sequentialTargetsRelay);
        return sequentialTargetsRelay;
    }

    public static MultiBlockLinkerBase<T> AddLink<T>(this MultiBlockLinkerBase<T> linker, T pipeBlock)
        where T : IPipeBlock
    {
        linker.LinkTo(pipeBlock);
        return linker;
    }

    public static IPipeRelay<TIn, TOut> To<TIn, TOut>(this IPipeTargetLinker<TIn> linker, IPipeRelay<TIn, TOut> target)
    {
        linker.LinkTo(target);
        return target;
    }

    public static IPipeTarget<T> To<T>(this IPipeTargetLinker<T> linker, IPipeTarget<T> target)
    {
        linker.LinkTo(target);
        return target;
    }

    public static IPipeRelay<T, T> Union<T>(
        this IPipeTargetLinker<T> baseLinkerBlock,
        params IPipeTargetLinker<T>[] linkerBlocks
    )
    {
        var hub = new EmptyRelay<T>();
        baseLinkerBlock.LinkTo(hub);
        foreach (var linkerBlock in linkerBlocks)
            linkerBlock.LinkTo(hub);
        return hub;
    }

    public static IPipeRelay<TIn, TOut> Map<TIn, TOut>(
        this IPipeTargetLinker<TIn> relay,
        Func<TIn, TOut> map)
    {
        return relay.To(new MapperBlock<TIn, TOut>(map));
    }

    public static IPipeRelay<TIn, TOut> Map<TIn, TOut>(
        this IPipeTargetLinker<TIn> relay,
        Func<TIn, Task<TOut>> asyncMap)
    {
        return relay.To(new MapperBlock<TIn, TOut>(asyncMap));
    }

    public static IPipeRelay<T, T> Filter<T>(
        this IPipeTargetLinker<T> relay,
        Func<T, bool> filter)
    {
        return relay.To(new FilterBlock<T>(filter));
    }

    public static IPipeRelay<T, T> Filter<T>(
        this IPipeTargetLinker<T> relay,
        Func<T, Task<bool>> asyncFilter)
    {
        return relay.To(new FilterBlock<T>(asyncFilter));
    }
}