using DataPipes.Core.Abstractions.PipeBlocks.PullModel;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Events;

public interface ILinearizer<T> : IPipeSource<T>, IPipeTarget<T>;