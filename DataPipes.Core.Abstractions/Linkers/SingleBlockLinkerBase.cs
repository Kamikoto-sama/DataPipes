using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Linkers;

public abstract class SingleBlockLinkerBase<T> : IPipeLinker<T> where T : IPipeBlock
{
    public IReadOnlyCollection<T> LinkedBlocks => SingleBlock == null ? [] : [SingleBlock];

    protected T? SingleBlock;

    public virtual PipeBlockMeta Meta =>
        PipeBlockMetaBuilder.Create(this, (IReadOnlyCollection<IPipeBlock>)LinkedBlocks);

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
        if (EqualsToSingleBlock(pipeBlock))
            SingleBlock = default;
    }

    public virtual void UnlinkAll()
    {
        SingleBlock = default;
    }

    //TODO: Refactor
    private bool EqualsToSingleBlock(T otherBlock)
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        if (SingleBlock is IEquatable<T> equatable)
            return equatable.Equals(otherBlock);
        return EqualityComparer<T>.Default.Equals(otherBlock, SingleBlock);
    }
}