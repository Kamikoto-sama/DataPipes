namespace DataPipes.Pipelines.Abstractions;

public interface IKeyedItem
{
    string Key { get; }
}

public class KeyedItem<T>(string key, T payload) : IKeyedItem
{
    public string Key { get; } = key;
    public T Payload { get; } = payload;
}