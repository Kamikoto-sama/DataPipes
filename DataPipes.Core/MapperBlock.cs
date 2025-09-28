using System.Runtime.CompilerServices;
using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core;

public class MapperBlock<TIn, TOut> : SingleTargetRelayBase<TIn, TOut>
{
    private readonly string? name;
    public override PipeBlockMeta Meta => name != null ? base.Meta with { Name = name } : base.Meta;

    private readonly Func<TIn, Task<TOut>>? asyncMapper;
    private readonly Func<TIn, TOut>? syncMapper;

    public MapperBlock(Func<TIn, TOut> syncMapper, [CallerArgumentExpression(nameof(syncMapper))] string? name = null)
        : this(name)
    {
        this.syncMapper = syncMapper;
    }

    public MapperBlock(
        Func<TIn, Task<TOut>> asyncMapper,
        [CallerArgumentExpression(nameof(asyncMapper))]
        string? name = null)
        : this(name)
    {
        this.asyncMapper = asyncMapper;
    }

    private MapperBlock(string? name)
    {
        this.name = name;
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