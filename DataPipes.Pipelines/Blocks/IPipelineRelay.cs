using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Pipelines.Blocks;

public interface IPipelineRelay<TIn, TOut> : IPipeRelay<PipelinePayload<TIn>, PipelinePayload<TOut>>;