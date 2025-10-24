using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.Meta;
using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;

namespace DataPipes.Core.Blocks;

public class PipeSourcePropagator<T>(IPipeSource<T> source) : SingleTargetLinkerBase<T>, IPipeRunner
{
    public override PipeBlockMeta Meta => PipeBlockMetaFactory.Create(this, [source, SingleBlock]);

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
                return;

            var payload = result.Payload!;
            if (target != null)
                await target.HandlePayload(payload, cancellationToken);

            await source.Commit(result);
        }
    }
}