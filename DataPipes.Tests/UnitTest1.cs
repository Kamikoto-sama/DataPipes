using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var source = GetSource<string>();
        source
            .Map(x => 10);
    }

    private static IPipeRelay<TIn, TOut> GetRelay<TIn, TOut>() => throw new NotImplementedException();

    private static IPipeSource<T> GetSource<T>() => throw new NotImplementedException();

    private static IPipeTarget<T> GetTarget<T>() => throw new NotImplementedException();
}