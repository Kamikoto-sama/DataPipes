using DataPipes.Core.Abstractions.PipeBlocks.PushModel;

namespace DataPipes.Core.Abstractions.Linkers;

public abstract class MultiTargetLinkerBase<T> : MultiBlockLinkerBase<IPipeTarget<T>>, IPipeTargetLinker<T>;