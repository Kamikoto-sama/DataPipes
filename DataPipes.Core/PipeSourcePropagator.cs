using DataPipes.Core.Abstractions.Linkers;
using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core;

public class PipeSourcePropagator<T>(IPipeSource<T> source) : SingleBlockLinkerBase<IPipeTarget<T>>, IPipeRunner
{
    public override Task Initialize(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Run(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}