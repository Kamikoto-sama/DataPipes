using DataPipes.Core.Abstractions;
using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.PipeTopology;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.JointBlocks;

namespace DataPipes.Pipelines.Extensions;

public static class PipelineRailingExtensions
{
    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> ReadFrom<T>(
        this PipelineContext context,
        IPipeSource<PipelinePayload<T>> source)
    {
        var propagator = new PipelineSourcePropagator<T>(source);
        return new PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>>([propagator], propagator, context);
    }

    public static IPipelineRailing<IPipeRelay<TIn, TOut>> To<TIn, TOut>(
        this IPipelineRailing<IPipeTargetLinker<TIn>> source,
        IPipeRelay<TIn, TOut> target)
    {
        source.TailBlock.LinkTo(target);
        return source.TailWith(target);
    }

    public static IPipelineRailing<IPipeTarget<T>> To<T>(
        this IPipelineRailing<IPipeTargetLinker<T>> source,
        IPipeTarget<T> target)
    {
        source.TailBlock.LinkTo(target);
        return source.TailWith(target);
    }

    public static async Task ExecuteAsync(
        this IPipelineRailing<IPipeBlock> railing,
        CancellationToken cancellationToken)
    {
        new PipeTopologyExplorer().Explore(railing.EntryBlocks.ToArray<IPipeBlock>());
        var pipeline = new Pipeline(railing.Context, railing.EntryBlocks);
        await pipeline.Initialize(cancellationToken);
        await pipeline.Run(cancellationToken);
    }
}