namespace DataPipes.Events;

public class PipeKeyedItem<TPayload, TContext>
{
    public required PipeEvent<PipeEvent<TPayload>>[] EventsBatch { get; init; }
    public TContext? Context { get; init; }
}