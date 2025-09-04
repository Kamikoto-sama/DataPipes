namespace DataPipes.Core.Pipelines;

public interface IPipeline<TIn, TOut>
{
    void Initialize();
    void Run();
}