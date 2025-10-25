using DataPipes.Core.Abstractions;

namespace DataPipes.Core.PipeTopology;

public interface IPipeTopologyExplorer
{
    PipeTopology Explore(params IPipeBlock[] pipeEntryBlocks);
}