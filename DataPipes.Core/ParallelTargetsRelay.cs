using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core;

public class ParallelTargetsRelay<T>(int degreeOfParallelism) : ParallelTargetsRelayBase<T, T>(degreeOfParallelism)
{
    public override Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected override async Task HandleEvent(T payload, IPipeTarget<T>? target, CancellationToken cancellationToken)
    {
        if (target != null)
            await target.HandleEvent(payload, cancellationToken);
    }
}