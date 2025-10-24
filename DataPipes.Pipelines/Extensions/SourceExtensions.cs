using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.BuiltIn.Sources;

namespace DataPipes.Pipelines.Extensions;

public static class SourceExtensions
{
    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> ReadFromEnumerable<T>(
        this PipelineContext context,
        IEnumerable<T> enumerable, bool readToEnd)
    {
        return context.ReadFrom(new EnumerablePipeSource<T>(enumerable, readToEnd));
    }

    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<string>>> ReadFromFile(
        this PipelineContext context,
        string filePath, bool readToEnd)
    {
        return context.ReadFrom(new FilePipeSource(filePath, readToEnd));
    }
}