namespace DataPipes.Core.Abstractions.PipeBlocks.PullModel;

public interface IPipeReader<TIn, TOut> : IPipeLinker<IPipeSource<TIn>>, IPipeSource<TOut>;