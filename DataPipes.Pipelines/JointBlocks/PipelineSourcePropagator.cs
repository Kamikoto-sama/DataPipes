using DataPipes.Core.Abstractions;
using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Linkers;
using DataPipes.Core.Meta;
using DataPipes.Pipelines.Abstractions;
using DataPipes.Pipelines.Abstractions.Blocks;

namespace DataPipes.Pipelines.JointBlocks;

public class PipelineSourcePropagator<T>(IPipeSource<PipelinePayload<T>> source, string? name = null)
    : SingleTargetLinkerBase<PipelinePayload<T>>, IFinitePipeRunner
{
    public override PipeBlockMeta Meta => PipeBlockMetaFactory.Create(
        name ?? GetType().Name,
        [source, SingleBlock]
    );

    public event Action? OnFinished;

    public override async Task Initialize(CancellationToken cancellationToken)
    {
        await source.Initialize(cancellationToken);
        await base.Initialize(cancellationToken);
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        var target = SingleBlock;
        while (!cancellationToken.IsCancellationRequested)
        {
            var result = await source.Consume(cancellationToken);
            if (result.EndOfSource)
            {
                OnFinished?.Invoke();
                return;
            }

            var payload = result.Payload!;
            if (target != null)
                await target.HandlePayload(payload, cancellationToken);

            await source.Commit(result);
        }
    }
}