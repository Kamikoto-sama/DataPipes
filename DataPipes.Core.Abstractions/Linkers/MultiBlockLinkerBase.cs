using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.Abstractions.Linkers;

public abstract class MultiBlockLinkerBase<T> : IPipeLinker<T> where T : IPipeBlock
{
    public virtual IReadOnlyCollection<T> LinkedBlocks => Blocks;

    protected readonly List<T> Blocks = [];

    public virtual PipeBlockMeta Meta =>
        PipeBlockMetaBuilder.Create(this, (IReadOnlyCollection<IPipeBlock>)LinkedBlocks);

    public virtual async Task Initialize(CancellationToken cancellationToken)
    {
        foreach (var pipeBlock in Blocks)
            await pipeBlock.Initialize(cancellationToken);
    }

    public virtual void LinkTo(T pipeBlock)
    {
        Blocks.Add(pipeBlock);
    }

    public virtual void Unlink(T pipeBlock)
    {
        Blocks.Remove(pipeBlock);
    }

    public virtual void UnlinkAll()
    {
        Blocks.Clear();
    }
}