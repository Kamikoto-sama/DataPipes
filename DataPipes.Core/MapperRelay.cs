using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core;

public class TransformBlock<TIn, TOut> : SingleTargetRelay<TIn, TOut>
{
    private readonly Func<TIn, Task<TOut>>? asyncMapper;
    private readonly Func<TIn, TOut>? syncMapper;

    public TransformBlock(Func<TIn, TOut> syncMapper) => this.syncMapper = syncMapper;

    public TransformBlock(Func<TIn, Task<TOut>> asyncMapper) => this.asyncMapper = asyncMapper;

    public override Task Initialize() => Task.CompletedTask;

    protected override async Task HandleEvent(TIn payload, IPipeTarget<TOut>? target)
    {
        if (syncMapper != null)
            target?.HandleEvent(syncMapper.Invoke(payload));
        if (asyncMapper != null)
            target?.HandleEvent(await asyncMapper.Invoke(payload));
    }
}
