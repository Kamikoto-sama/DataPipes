using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IPipelineTarget<T> : IPipeTarget<PipelinePayload<T>>;