using DataPipes.Core.Abstractions;
using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Abstractions.Blocks;
using DataPipes.Pipelines.Linearizing;

namespace DataPipes.Pipelines.Extensions;

public static class MergeExtensions
{
    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> MergeWith<T>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> source1,
        IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> source2)
    {
        var linearizer = new LocalLinearizer<PipelinePayload<T>>();
        var sourceEntryBlocks = source1.EntryBlocks.Concat(source2.EntryBlocks).ToArray();
        SubscribeOnFinished(sourceEntryBlocks, linearizer);

        source1.TailBlock.LinkTo(linearizer);
        source2.TailBlock.LinkTo(linearizer);
        var newPipe = source1.Context.ReadFrom(linearizer);
        var entryBlocks = sourceEntryBlocks.Concat(newPipe.EntryBlocks).ToArray();
        return new PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>>(
            entryBlocks,
            newPipe.TailBlock,
            source1.Context);
    }

    private static void SubscribeOnFinished<T>(
        IPipeRunner[] sourceEntryBlocks,
        LocalLinearizer<PipelinePayload<T>> linearizer)
    {
        if (sourceEntryBlocks.Any(x => x is not IFinitePipeRunner))
            return;

        var state = new SourcesState { Count = sourceEntryBlocks.Length };
        foreach (var runner in sourceEntryBlocks.Cast<IFinitePipeRunner>())
            runner.OnFinished += () =>
            {
                state.Count--;
                if (state.Count <= 0)
                    linearizer.Complete();
            };
    }

    private class SourcesState
    {
        public int Count { get; set; }
    }
}