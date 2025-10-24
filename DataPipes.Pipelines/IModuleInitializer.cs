namespace DataPipes.Pipelines;

public interface IModuleInitializer
{
    int Order { get; }
    Task Initialize(CancellationToken cancellationToken);
}