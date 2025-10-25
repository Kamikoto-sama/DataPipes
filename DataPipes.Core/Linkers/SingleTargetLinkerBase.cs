using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Core.Linkers;

public abstract class SingleTargetLinkerBase<T> : SingleBlockLinkerBase<IPipeTarget<T>>, IPipeTargetLinker<T>;