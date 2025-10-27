using DataPipes.Core.Abstractions;
using DataPipes.Pipelines.Abstractions;

namespace DataPipes.Pipelines;

public class PipelineRailing<TTail>(
    IReadOnlyCollection<IPipeRunner> entryBlocks,
    TTail tailBlock,
    PipelineContext context)
    : IPipelineRailing<TTail> where TTail : IPipeBlock
{
    public IReadOnlyCollection<IPipeRunner> EntryBlocks { get; } = entryBlocks;
    public TTail TailBlock { get; } = tailBlock;
    public PipelineContext Context { get; } = context;

    public IPipelineRailing<T2> TailWith<T2>(T2 tailBlock) where T2 : IPipeBlock
    {
        return new PipelineRailing<T2>(EntryBlocks, tailBlock, Context);
    }
}