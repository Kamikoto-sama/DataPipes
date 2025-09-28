namespace DataPipes.Events;

public record PipelineSourceEvent<T>(string Id, string SourceName) : PipeEvent<T>(Id);