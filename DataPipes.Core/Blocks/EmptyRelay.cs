using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core.Blocks;

public class EmptyRelay<T>(string? name = null) : SingleTargetRelayBase<T, T>
{
    public override PipeBlockMeta Meta => name == null ? base.Meta : PipeBlockMetaFactory.Create(name, [SingleBlock]);

    protected override async Task HandlePayload(T payload, IPipeTarget<T>? target, CancellationToken cancellationToken)
    {
        if (target != null)
            await target.HandlePayload(payload, cancellationToken);
    }
}