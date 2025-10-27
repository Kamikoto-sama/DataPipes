using DataPipes.Core.Abstractions;

namespace DataPipes.Pipelines.Abstractions.Blocks;

public interface IFinitePipeRunner : IPipeRunner
{
    event Action OnFinished;
}