using DataPipes.Core.Abstractions.PullModel;

namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IPipelineSourceConsumeResult<T> : IPipeSourceConsumeResult<PipelinePayload<T>>;