using DataPipes.Core.Targets;

namespace DataPipes.Core.EdgeBlocks.Targets;

public class ConsolePipeTarget : PipeTargetBase<string>
{
    public override Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override Task HandlePayload(string payload, CancellationToken cancellationToken)
    {
        Console.WriteLine(payload);
        return Task.CompletedTask;
    }
}
