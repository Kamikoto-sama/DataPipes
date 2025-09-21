using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core;

public class SequentialTargetsRelay<T> : SequentialTargetsRelayBase<T, T>
{
    protected override async Task HandlePayload(T payload, IPipeTarget<T>? target, CancellationToken cancellationToken)
    {
        if (target != null)
            await target.HandlePayload(payload, cancellationToken);
    }
}