using DataPipes.Core.Abstractions.PipeBlocks;
using DataPipes.Core.Abstractions.Relays;

namespace DataPipes.Core;

public class PipeSourcePropagator<T>(IPipeSource<T> source) : SingleTargetRelay<T, T>, IPipeRunner, IDisposable
{
    public override Task Initialize(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Run(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task HandleEvent(T payload, IPipeTarget<T>? target, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}