using DataPipes.Core.Abstractions;

namespace Sandbox;

public record PipelineBlockMeta(string Name) : PipeBlockMeta(Name)
{
}

public enum PipelineBlockType
{
    Custom,
    Relay,
    Source,
    Sink
}