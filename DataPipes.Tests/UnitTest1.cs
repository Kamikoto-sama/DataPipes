using DataPipes.Core;
using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Tests;

public class Tests
{
    [Test]
    public async Task Test1()
    {
        using var source = new EnumerableSource<int>([1, 2, 3]);
        var mapper = new MapperBlock<int, string>(i => i.ToString());
        var mapper2 = new MapperBlock<string, string>(i => i + ".file");
    }

    private static IPipeRelay<TIn, TOut> GetRelay<TIn, TOut>() => throw new NotImplementedException();

    private static IPipeSource<T> GetSource<T>() => throw new NotImplementedException();

    private static IPipeTarget<T> GetTarget<T>() => throw new NotImplementedException();
}
