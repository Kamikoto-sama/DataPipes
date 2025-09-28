using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core.PipeTopology;

public record PipeTopology(IPipeBlock[] Blocks, PipeTopologyLink[] BlockLinks);