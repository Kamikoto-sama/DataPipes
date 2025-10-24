using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Pipelines.Blocks;

public interface IPipelineSink<T> : IPipeTarget<PipelinePayload<T>>;