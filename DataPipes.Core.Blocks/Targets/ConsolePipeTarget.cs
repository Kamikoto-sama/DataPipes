using DataPipes.Core.Abstractions.Targets;

namespace DataPipes.Core.Blocks.Targets;

public class ConsolePipeTarget<T> : PipeTargetBase<T>
{
    public override Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override Task HandlePayload(T payload, CancellationToken cancellationToken)
    {
        Console.WriteLine(payload);
        return Task.CompletedTask;
    }
}