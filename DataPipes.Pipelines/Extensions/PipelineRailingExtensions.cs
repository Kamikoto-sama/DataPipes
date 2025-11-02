using System.Text.Json;
using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Pipelines.Abstractions;
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

    public static IPipelineRailing<IPipeRelay<PipelinePayload<T>, PipelinePayload<KeyedItem<T>>>> KeyBy<T, TKey>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> source,
        Func<T, TKey> keySelector)
    {
        return source.Map(payload =>
        {
            var key = JsonSerializer.Serialize(keySelector(payload));
            return new KeyedItem<T>(key, payload);
        });
    }
}