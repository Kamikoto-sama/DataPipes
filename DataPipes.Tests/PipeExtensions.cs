using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Tests;

public static class PipeExtensions
{
    public static IPipeRelay<TIn, TOut> Map<TIn, TOut>(this IPipeSource<TIn> source, Func<TIn, TOut> map)
    {
        throw new NotImplementedException();
    }

    public static IPipeRelay<TOut1, TOut2> Map<TIn, TOut1, TOut2>(this IPipeRelay<TIn, TOut1> source, Func<TOut1, TOut2> map)
    {
        throw new NotImplementedException();
    }
}