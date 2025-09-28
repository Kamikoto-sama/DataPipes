namespace DataPipes.Events;

public class PipeItem<TPayload, TContext>
{
    public required PipeEvent<TPayload>[] EventsBatch { get; init; }
    public TContext? Context { get; init; }
}