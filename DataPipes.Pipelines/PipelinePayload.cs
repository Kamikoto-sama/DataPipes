namespace DataPipes.Pipelines;

public record PipelinePayload<T>(T[] ItemsBatch, PipelineContext Context);