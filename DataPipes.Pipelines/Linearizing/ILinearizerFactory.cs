using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Pipelines.Linearizing;

public interface ILinearizerFactory
{
    (IPipeTarget<T> Input, IPipeSource<T> Output) Create<T>();
}

public class LocalLinearizerFactory : ILinearizerFactory
{
    public (IPipeTarget<T> Input, IPipeSource<T> Output) Create<T>()
    {
        var linearizer = new LocalLinearizer<T>();
        return (linearizer, linearizer);
    }
}