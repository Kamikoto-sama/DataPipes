using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Linkers;

public abstract class SingleTargetLinkerBase<T> : SingleBlockLinkerBase<IPipeTarget<T>>, IPipeTargetLinker<T>;