namespace DataPipes.Core.Abstractions.PullModel;

public interface IPipeReader<TIn, TOut> : IPipeLinker<IPipeSource<TIn>>, IPipeSource<TOut>;