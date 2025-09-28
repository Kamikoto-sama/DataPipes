using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Linkers;

public abstract class SingleBlockLinkerBase<T> : IPipeLinker<T> where T : IPipeBlock
{
    public IReadOnlyCollection<T> LinkedBlocks => SingleBlock == null ? [] : [SingleBlock];
    public virtual PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this, [SingleBlock]);

    protected T? SingleBlock;

    public virtual async Task Initialize(CancellationToken cancellationToken)
    {
        if (SingleBlock != null)
            await SingleBlock.Initialize(cancellationToken);
    }

    public virtual void LinkTo(T pipeBlock)
    {
        if (SingleBlock != null)
            throw new InvalidOperationException($"Block is already linked to '{pipeBlock}'");
        SingleBlock = pipeBlock;
    }

    public virtual void Unlink(T pipeBlock)
    {
        if (EqualityComparer<T>.Default.Equals(pipeBlock, SingleBlock))
            SingleBlock = default;
    }

    public virtual void UnlinkAll()
    {
        SingleBlock = default;
    }
}