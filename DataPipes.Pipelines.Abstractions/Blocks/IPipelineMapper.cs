namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IPipelineMapper<in TIn, TOut>
{
    Task<TOut> Map(TIn input);
}