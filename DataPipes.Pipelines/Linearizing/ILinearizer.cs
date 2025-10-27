using DataPipes.Core.Abstractions.PullModel;
using DataPipes.Core.Abstractions.PushModel;

namespace DataPipes.Pipelines.Linearizing;

public interface ILinearizer<T> : IPipeSource<T>, IPipeTarget<T>;