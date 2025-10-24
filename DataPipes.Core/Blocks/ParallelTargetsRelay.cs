using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core.Blocks;

public class ParallelTargetsRelay<T>(int degreeOfParallelism) : ParallelTargetsRelayBase<T, T>(degreeOfParallelism)
{
    protected override async Task HandlePayload(T payload, IPipeTarget<T>? target, CancellationToken cancellationToken)
    {
        if (target != null)
            await target.HandlePayload(payload, cancellationToken);
    }
}