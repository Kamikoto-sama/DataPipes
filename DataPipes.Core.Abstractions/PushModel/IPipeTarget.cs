namespace DataPipes.Core.Abstractions.PushModel;

public interface IPipeTarget<in T> : IPipeBlock
{
    Task HandlePayload(T payload, CancellationToken cancellationToken);
}