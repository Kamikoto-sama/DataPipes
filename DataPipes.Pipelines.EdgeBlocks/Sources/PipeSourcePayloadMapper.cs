using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Readers;
using DataPipes.Core.Sources;
using DataPipes.Pipelines.Abstractions;

namespace DataPipes.Pipelines.EdgeBlocks.Sources;

public class PipeSourcePayloadMapper<T>(PipelineContext context)
    : SingleSourceReaderBase<T[], PipelinePayload<T>>
{
    protected override async Task<IPipeSourceConsumeResult<PipelinePayload<T>>> Consume(
        IPipeSource<T[]> pipeSource,
        CancellationToken cancellationToken)
    {
        var result = await pipeSource.Consume(cancellationToken);
        if (result.EndOfSource)
            return new ConsumeResult(null, true, null);
        var payload = new PipelinePayload<T>(result.Payload!, context);
        return new ConsumeResult(payload, false, result);
    }

    protected override async Task Commit(
        IPipeSource<T[]> pipeSource,
        IPipeSourceConsumeResult<PipelinePayload<T>> consumeResult)
    {
        var result = PipeSourceBase<PipelinePayload<T>>.EnsureResultType<ConsumeResult>(consumeResult, true);
        await pipeSource.Commit(result.SourceResult!);
    }

    private record ConsumeResult(
        PipelinePayload<T>? Payload,
        bool EndOfSource,
        IPipeSourceConsumeResult<T[]>? SourceResult)
        : IPipeSourceConsumeResult<PipelinePayload<T>>;
}