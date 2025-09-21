using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core;

public class MapperBlock<TIn, TOut> : SingleTargetRelayBase<TIn, TOut>
{
    private readonly Func<TIn, Task<TOut>>? asyncMapper;
    private readonly Func<TIn, TOut>? syncMapper;

    public MapperBlock(Func<TIn, TOut> syncMapper)
    {
        this.syncMapper = syncMapper;
    }

    public MapperBlock(Func<TIn, Task<TOut>> asyncMapper)
    {
        this.asyncMapper = asyncMapper;
    }

    protected override async Task HandlePayload(
        TIn payload,
        IPipeTarget<TOut>? target,
        CancellationToken cancellationToken)
    {
        if (asyncMapper != null)
            if (target != null)
                await target.HandlePayload(await asyncMapper(payload), cancellationToken);
        if (syncMapper != null)
            if (target != null)
                await target.HandlePayload(syncMapper(payload), cancellationToken);
    }
}