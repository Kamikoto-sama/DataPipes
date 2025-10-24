using DataPipes.Core.Abstractions.PipeBlocks.PullModel;

namespace DataPipes.Core.Abstractions;

public static class SourceUtil
{
    public static T2 EnsureResultType<T1, T2>(IPipeSourceConsumeResult<T1> result)
    {
        if (result is T2 expectedResult)
            return expectedResult;
        throw new ArgumentException($"Expected result of type '{nameof(T2)}', but got type '{result.GetType()}'");
    }
}