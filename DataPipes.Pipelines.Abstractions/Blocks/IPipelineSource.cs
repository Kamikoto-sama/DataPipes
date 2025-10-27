using DataPipes.Core.Abstractions.PullModel;

namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IPipelineSource<T> : IPipeSource<PipelinePayload<T>>;