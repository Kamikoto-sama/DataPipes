namespace DataPipes.Core;

public record PipeEvent<T>
{
    public required string Id { get; init; }
    public T? Payload { get; init; }
}