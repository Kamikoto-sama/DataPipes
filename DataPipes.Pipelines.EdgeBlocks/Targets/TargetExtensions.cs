using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Extensions;

namespace DataPipes.Pipelines.EdgeBlocks.Targets;

public static class TargetExtensions
{
    public static IPipelineRailing<IPipeTarget<PipelinePayload<string>>> SinkToConsole(
        this IPipelineRailing<IPipeTargetLinker<PipelinePayload<string>>> source,
        bool printAsError = false)
    {
        var options = new ConsolePipelineSinkOptions { PrintAsError = printAsError };
        return source.To(new ConsolePipelineSink(options));
    }
}