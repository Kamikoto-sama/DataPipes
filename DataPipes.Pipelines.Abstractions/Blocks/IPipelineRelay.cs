using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IPipelineRelay<TIn, TOut> : IPipeRelay<PipelinePayload<TIn>, PipelinePayload<TOut>>;