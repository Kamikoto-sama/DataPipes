using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Pipelines.Blocks;

namespace DataPipes.Pipelines.Extensions;

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