using System.Diagnostics.CodeAnalysis;

namespace DataPipes.Events;

public record PipeEvent<T>
{
    public required string Id { get; init; }

    [MemberNotNullWhen(false, nameof(Deleted))]
    public T? Payload { get; init; }

    public bool Deleted { get; init; }
}