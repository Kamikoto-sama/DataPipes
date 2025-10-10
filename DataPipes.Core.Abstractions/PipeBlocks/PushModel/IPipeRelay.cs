namespace DataPipes.Core.Abstractions.PipeBlocks.PushModel;

public interface IPipeRelay<in TIn, TOut> : IPipeTargetLinker<TOut>, IPipeTarget<TIn>;