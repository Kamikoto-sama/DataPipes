using DataPipes.Pipelines.Linearizing;
using Microsoft.Extensions.DependencyInjection;

namespace DataPipes.Pipelines.App;

public class PipelineContextBuilder(IServiceCollection services)
{
    public PipelineContextBuilder SetRemoteLinearizer<T>() where T : class, ILinearizerFactory
    {
        services.AddKeyedSingleton<ILinearizerFactory, T>(LinearizerType.Remote);
        return this;
    }

    public static PipelineContextBuilder CreateDefault()
    {
        return new PipelineContextBuilder(new ServiceCollection());
    }
}