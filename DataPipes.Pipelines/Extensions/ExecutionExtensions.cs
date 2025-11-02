using DataPipes.Core.Abstractions;
using DataPipes.Core.PipeTopology;
using DataPipes.Pipelines.Abstractions;

namespace DataPipes.Pipelines.Extensions;

public static class ExecutionExtensions
{
    public static async Task ExecuteAsync(
        this IPipelineRailing<IPipeBlock> railing,
        CancellationToken cancellationToken)
    {
        new PipeTopologyExplorer().Explore(railing.EntryBlocks.ToArray<IPipeBlock>());
        var pipeline = new Pipeline(railing.Context, railing.EntryBlocks);
        await pipeline.Initialize(cancellationToken);
        await pipeline.Run(cancellationToken);
    }
}