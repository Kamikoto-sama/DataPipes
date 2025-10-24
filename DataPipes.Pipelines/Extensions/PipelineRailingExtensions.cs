using DataPipes.Core;
using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.PipeTopology;
using DataPipes.Pipelines.Blocks;

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

    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> ReadFrom<T>(
        this PipelineContext context,
        IPipeSource<T> source)
    {
        var mapper = new PipelinePayloadSourceMapper<T>(context);
        mapper.LinkTo(source);
        return context.ReadFrom(mapper);
    }

    public static IPipelineRailing<IPipeRelay<TIn, TOut>> To<TIn, TOut>(
        this IPipelineRailing<IPipeTargetLinker<TIn>> source,
        IPipeRelay<TIn, TOut> target)
    {
        return source.TailWith(source.TailBlock.To(target));
    }

    public static IPipelineRailing<IPipeTarget<T>> To<T>(
        this IPipelineRailing<IPipeTargetLinker<T>> source,
        IPipeTarget<T> target)
    {
        return source.TailWith(source.TailBlock.To(target));
    }

    public static async Task ExecuteAsync(
        this IPipelineRailing<IPipeBlock> railing,
        CancellationToken cancellationToken)
    {
        new DefaultPipeTopologyExplorer().Explore(railing.EntryBlocks.ToArray<IPipeBlock>());
        var pipeline = new Pipeline(railing.Context, railing.EntryBlocks);
        await pipeline.Initialize(cancellationToken);
        await pipeline.Run(cancellationToken);
    }
}