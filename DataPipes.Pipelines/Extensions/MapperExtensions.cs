using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Abstractions.Blocks;
using DataPipes.Pipelines.Blocks;
using Microsoft.Extensions.DependencyInjection;

namespace DataPipes.Pipelines.Extensions;

public static class MapperExtensions
{
    public static IPipelineRailing<IPipeRelay<PipelinePayload<TIn>, PipelinePayload<TOut>>> Map<TIn, TOut>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<TIn>>> source,
        Func<TIn, Task<TOut>> mapper)
    {
        var mapperBlock = new PipelineMapperBlock<TIn, TOut>(async payload =>
        {
            var items = new List<TOut>();
            foreach (var item in payload.ItemsBatch)
                items.Add(await mapper(item));
            return new PipelinePayload<TOut>(items.ToArray(), payload.Context);
        });
        return source.To(mapperBlock);
    }

    public static IPipelineRailing<IPipeRelay<PipelinePayload<TIn>, PipelinePayload<TOut>>> Map<TIn, TOut>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<TIn>>> source,
        Func<TIn, TOut> mapper)
    {
        return source.Map(p => Task.FromResult(mapper(p)));
    }

    public static IPipelineRailing<IPipeRelay<PipelinePayload<TIn>, PipelinePayload<TOut>>> Map<TIn, TOut>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<TIn>>> source,
        IPipelineMapper<TIn, TOut> mapperImpl)
    {
        var mapperBlock = new PipelineMapperBlock<TIn, TOut>(async payload =>
        {
            var items = new List<TOut>();
            foreach (var item in payload.ItemsBatch)
                items.Add(await mapperImpl.Map(item));
            return new PipelinePayload<TOut>(items.ToArray(), payload.Context);
        });
        return source.To(mapperBlock);
    }

    public static IPipelineRailing<IPipeRelay<PipelinePayload<TIn>, PipelinePayload<TOut>>> Map<TIn, TOut, TMapper>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<TIn>>> source)
        where TMapper : IPipelineMapper<TIn, TOut>
    {
        var mapperBlock = new PipelineMapperBlock<TIn, TOut>(async payload =>
        {
            var mapper = payload.Context.Services.GetRequiredService<TMapper>();
            var items = new List<TOut>();
            foreach (var item in payload.ItemsBatch)
                items.Add(await mapper.Map(item));
            return new PipelinePayload<TOut>(items.ToArray(), payload.Context);
        });
        return source.To(mapperBlock);
    }
}