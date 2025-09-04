using DataPipes.Core.Abstractions.PipeBlocks;

namespace DataPipes.Core;

public interface ILinearizer<T> : IPipeTarget<T>, IPipeSource<T>;