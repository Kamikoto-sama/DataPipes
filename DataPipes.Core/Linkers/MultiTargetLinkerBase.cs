using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Core.Linkers;

public abstract class MultiTargetLinkerBase<T> : MultiBlockLinkerBase<IPipeTarget<T>>, IPipeTargetLinker<T>;