using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DataPipes.Pipelines.Linearizing;

public static class LinearizingExtensions
{
    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> Linearize<T>(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> railing)
        where T : IKeyedItem
    {
        var context = railing.Context;
        var factory =
            context.Services.GetRequiredKeyedService<ILinearizerFactory<PipelinePayload<T>>>(LinearizerType.Remote);
        var (target, source) = factory.Create();
        railing.TailBlock.LinkTo(target);
        return context.ReadFrom(source);
    }
}