using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IPipelineTargetLinker<T> : IPipeTargetLinker<PipelinePayload<T>>;