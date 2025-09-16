using System.Diagnostics.CodeAnalysis;

namespace DataPipes.Core.Abstractions.PipeBlocks.PullModel;

public interface IPipeSourceConsumeResult<out T>
{
    [MemberNotNullWhen(false, nameof(EndOfSource))]
    T? Payload { get; }

    bool EndOfSource { get; }
}