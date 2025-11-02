using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Pipelines.Linearizing;

public interface ILinearizerFactory<T>
{
    (IPipeTarget<T> Input, IPipeSource<T> Output) Create();
}

public class LocalLinearizerFactory<T> : ILinearizerFactory<T>
{
    public (IPipeTarget<T> Input, IPipeSource<T> Output) Create()
    {
        var linearizer = new LocalLinearizer<T>();
        return (linearizer, linearizer);
    }
}