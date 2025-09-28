using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.PipeTopology;

public interface IPipeTopologyExplorer
{
    PipeTopology Explore(IPipeBlock pipeEntryBlock);
}