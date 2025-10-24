using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Pipelines.Blocks;

namespace DataPipes.Pipelines.Extensions;

public static class UnionExtensions
{
    public static IPipelineRailing<IPipelineRelay<T, T>> UnionWith<T>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> source,
        IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> pipe)
    {
        var hub = new PipelineRelay<T>();
        source.TailBlock.LinkTo(hub);
        pipe.TailBlock.LinkTo(hub);
        var entryBlocks = source.EntryBlocks.Concat(pipe.EntryBlocks).ToArray();
        return new PipelineRailing<IPipelineRelay<T, T>>(entryBlocks, hub, pipe.Context);
    }

    public static IPipelineRailing<IPipelineRelay<T, T>> UnionWith<T>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> source,
        Func<PipelineContext, IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>>> pipeBuilder)
    {
        return source.UnionWith(pipeBuilder(source.Context));
    }

    public static IPipelineRailing<IPipelineRelay<T, T>> AndFrom<T>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> railing,
        IPipeSource<T> pipeSource)
    {
        var pipe = railing.Context.ReadFrom(pipeSource);
        return railing.UnionWith(pipe);
    }
}