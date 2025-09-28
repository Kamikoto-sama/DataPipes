using DataPipes.Core.Abstractions.Meta;

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