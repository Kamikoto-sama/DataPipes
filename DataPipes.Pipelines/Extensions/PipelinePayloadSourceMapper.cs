using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.Readers;

namespace DataPipes.Pipelines.Extensions;

public class PipelinePayloadSourceMapper<T>(PipelineContext context) : SingleSourceReaderBase<T, PipelinePayload<T>>
{
    protected override async Task<IPipeSourceConsumeResult<PipelinePayload<T>>> Consume(
        IPipeSource<T>? pipeSource,
        CancellationToken cancellationToken)
    {
        if (pipeSource == null)
            throw new Exception("No source specified");

        var result = await pipeSource.Consume(cancellationToken);
        if (result.EndOfSource)
            return new MapperResult(null, true, result);

        var payload = new PipelinePayload<T>([result.Payload!], context);
        return new MapperResult(payload, false, result);
    }

    protected override async Task Commit(IPipeSource<T>? pipeSource,
        IPipeSourceConsumeResult<PipelinePayload<T>> consumeResult)
    {
        if (pipeSource == null)
            throw new Exception("No source specified");

        var result = (MapperResult)consumeResult;
        await pipeSource.Commit(result.SourceResult);
    }

    private record MapperResult(PipelinePayload<T>? Payload, bool EndOfSource, IPipeSourceConsumeResult<T> SourceResult)
        : IPipeSourceConsumeResult<PipelinePayload<T>>;
}