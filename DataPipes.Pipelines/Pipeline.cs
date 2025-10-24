using DataPipes.Core.Abstractions.PipeBlocks;
using Microsoft.Extensions.DependencyInjection;

namespace DataPipes.Pipelines;

public interface IPipeline
{
    PipelineContext Context { get; }
    IReadOnlyList<IPipeRunner> EntryBlocks { get; }
    Task Initialize(CancellationToken cancellationToken);
    Task Run(CancellationToken cancellationToken);
}

public class Pipeline(PipelineContext context, IEnumerable<IPipeRunner> pipeRunners) : IPipeline
{
    public PipelineContext Context { get; } = context;
    public IReadOnlyList<IPipeRunner> EntryBlocks { get; } = pipeRunners.ToArray();

    public async Task Initialize(CancellationToken cancellationToken)
    {
        var initializers = Context.Services.GetServices<IModuleInitializer>()
            .Select((init, index) => (init, index))
            .OrderBy(x => x.init.Order)
            .ThenBy(x => x.index);
        foreach (var initializer in initializers.Select(x => x.init))
            await initializer.Initialize(cancellationToken);
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        var runnerTasks = pipeRunners.Select(r => r.Run(cancellationToken));
        await Task.WhenAll(runnerTasks);
    }
}