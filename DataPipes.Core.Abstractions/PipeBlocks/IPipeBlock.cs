using DataPipes.Core.Abstractions.Meta;

namespace DataPipes.Core.Abstractions.PipeBlocks;

public interface IPipeBlock
{
    Task Initialize(CancellationToken cancellationToken);
    PipeBlockMeta Meta { get; }
}