using DataPipes.Core.Relays;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Abstractions.Blocks;

namespace DataPipes.Pipelines.JointBlocks;

public abstract class PipelineRelayBase<TIn, TOut>
    : SingleTargetRelayBase<PipelinePayload<TIn>, PipelinePayload<TOut>>, IPipelineRelay<TIn, TOut>;