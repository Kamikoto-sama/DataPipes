using DataPipes.Core.Targets;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Abstractions.Blocks;

namespace DataPipes.Pipelines.EdgeBlocks.Targets;

public class ConsolePipelineSink(ConsolePipelineSinkOptions options)
    : PipeTargetBase<PipelinePayload<string>>, IPipelineTarget<string>
{
    public override Task HandlePayload(PipelinePayload<string> payload, CancellationToken cancellationToken)
    {
        foreach (var item in payload.ItemsBatch)
            if (options.PrintAsError)
                Console.Error.WriteLine(item);
            else
                Console.WriteLine(item);

        return Task.CompletedTask;
    }
}

public class ConsolePipelineSinkOptions
{
    public bool PrintAsError { get; set; }
}