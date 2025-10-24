using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Pipelines.Blocks;

public abstract class PipelineRelayBase<TIn, TOut>
    : SingleTargetRelayBase<PipelinePayload<TIn>, PipelinePayload<TOut>>, IPipelineRelay<TIn, TOut>;