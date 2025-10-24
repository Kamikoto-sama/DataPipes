using DataPipes.Core;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Blocks;
using DataPipes.Pipelines.Blocks;

namespace DataPipes.Pipelines.Extensions;

public static class BranchExtensions
{
    public static IPipelineRailing<SequentialTargetsRelay<T>> BranchSequentially<T>(
        this IPipelineRailing<IPipeTargetLinker<T>> source)
    {
        return source.TailWith(source.TailBlock.BranchSequentially());
    }

    public static IPipelineRailing<SequentialTargetsRelay<T>> ToBranch<T>(
        this IPipelineRailing<SequentialTargetsRelay<T>> source,
        IPipelineSink<T> target)
    {
        source.TailBlock.LinkTo((IPipeTarget<T>)target);
        return source;
    }

    public static IPipelineRailing<SequentialTargetsRelay<T>> ToBranch<T>(
        this IPipelineRailing<SequentialTargetsRelay<T>> source,
        Action<IPipelineRailing<SequentialTargetsRelay<T>>> branch)
    {
        branch(source);
        return source;
    }
}