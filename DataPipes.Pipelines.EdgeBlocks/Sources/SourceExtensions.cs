using DataPipes.Core.Abstractions.PushModel;
using DataPipes.Core.EdgeBlocks.Sources;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Extensions;

namespace DataPipes.Pipelines.EdgeBlocks.Sources;

public static class SourceExtensions
{
    public static PipelineRailing<IPipeTargetLinker<PipelinePayload<T>>> ReadFromEnumerable<T>(
        this PipelineContext context,
        IEnumerable<T> enumerable,
        bool readToEnd,
        int batchSize)
    {
        var pipelineSource = new PipeSourcePayloadMapper<T>(context);
        var source = new EnumerablePipeSource<T[]>(enumerable.Batch(batchSize), readToEnd);
        pipelineSource.LinkTo(source);
        return context.ReadFrom(pipelineSource);
    }

    private static IEnumerable<T[]> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        var batch = new List<T>();
        foreach (var item in source)
        {
            if (batch.Count < batchSize)
            {
                batch.Add(item);
                continue;
            }

            yield return batch.ToArray();
            batch.Clear();
        }

        if (batch.Count > 0)
            yield return batch.ToArray();
    }
}