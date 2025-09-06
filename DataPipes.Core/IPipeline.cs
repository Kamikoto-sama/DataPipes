namespace DataPipes.Core;

public interface IPipeline<TIn, TOut>
{
    void Initialize();
    void Run();
}