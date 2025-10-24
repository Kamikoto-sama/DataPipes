using System.Runtime.CompilerServices;
using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core.Blocks;

public class FilterBlock<T> : SingleTargetRelayBase<T, T>
{
    public override PipeBlockMeta Meta => name != null ? base.Meta with { Name = name } : base.Meta;

    private readonly string? name;
    private readonly Func<T, bool>? syncFilter;
    private readonly Func<T, Task<bool>>? asyncFilter;

    public FilterBlock(Func<T, bool> syncFilter, [CallerArgumentExpression(nameof(syncFilter))] string? name = null)
        : this(name)
    {
        this.syncFilter = syncFilter;
    }

    public FilterBlock(
        Func<T, Task<bool>> asyncFilter,
        [CallerArgumentExpression(nameof(asyncFilter))]
        string? name = null) : this(name)
    {
        this.asyncFilter = asyncFilter;
    }

    private FilterBlock(string? name)
    {
        this.name = name;
    }

    protected override async Task HandlePayload(T payload, IPipeTarget<T>? target, CancellationToken cancellationToken)
    {
        if (target == null)
            return;

        if (syncFilter != null && syncFilter(payload))
            await target.HandlePayload(payload, cancellationToken);
        if (asyncFilter != null && await asyncFilter(payload))
            await target.HandlePayload(payload, cancellationToken);
    }
}