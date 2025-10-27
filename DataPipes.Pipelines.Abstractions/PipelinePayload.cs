namespace DataPipes.Pipelines.Abstractions;

public record PipelinePayload<T>(T[] ItemsBatch, PipelineContext Context);