namespace DataPipes.Core.Abstractions.PushModel;

public interface IPipeRelay<in TIn, TOut> : IPipeTargetLinker<TOut>, IPipeTarget<TIn>;