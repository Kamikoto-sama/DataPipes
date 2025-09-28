using System.Diagnostics.CodeAnalysis;

namespace DataPipes.Events;

public record PipeEvent<T>(string Id)
{
    [MemberNotNullWhen(false, nameof(Deleted))]
    public T? Payload { get; init; }

    public bool Deleted { get; init; }
}