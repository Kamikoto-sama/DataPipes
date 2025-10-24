using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Pipelines;

public interface IFinitePipeRunner : IPipeRunner
{
    event Action OnFinished;
}