namespace DataPipes.Core.Abstractions.PipeBlocks.PushModel;

public interface IPipeRelay<in TIn, TOut> : IPipeLinker<IPipeTarget<TOut>>, IPipeTarget<TIn>;