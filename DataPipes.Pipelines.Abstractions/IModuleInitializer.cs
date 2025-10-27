namespace DataPipes.Pipelines.Abstractions;

public interface IModuleInitializer
{
    int Order { get; }
    Task Initialize(CancellationToken cancellationToken);
}