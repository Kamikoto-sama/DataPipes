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

    protected override async Task HandleEvent(
        TIn payload,
        IPipeTarget<TOut>? target,
        CancellationToken cancellationToken)
    {
        if (syncMapper != null)
            target?.HandleEvent(syncMapper.Invoke(payload), cancellationToken);
        if (asyncMapper != null)
        {
            var task = target?.HandleEvent(await asyncMapper.Invoke(payload), cancellationToken);
            if (task != null)
                await task;
        }
    }
}