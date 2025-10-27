using DataPipes.Core.Abstractions;

namespace DataPipes.Pipelines.Abstractions;

public interface IPipelineRailing<out TTail> where TTail : IPipeBlock
{
    IReadOnlyCollection<IPipeRunner> EntryBlocks { get; }
    TTail TailBlock { get; }
    PipelineContext Context { get; }
    IPipelineRailing<T2> TailWith<T2>(T2 tailBlock) where T2 : IPipeBlock;
}