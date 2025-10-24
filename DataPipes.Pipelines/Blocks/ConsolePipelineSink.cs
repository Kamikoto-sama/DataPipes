using DataPipes.Core.Abstractions.Targets;

namespace DataPipes.Pipelines.Blocks;

public class ConsolePipelineSink(ConsolePipelineSinkOptions options)
    : PipeTargetBase<PipelinePayload<string>>, IPipelineSink<string>
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