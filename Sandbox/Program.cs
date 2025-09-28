using DataPipes.Core;
using DataPipes.Core.Abstractions.PipeBlocks.PushModel;
using DataPipes.Core.Blocks.Sources;
using DataPipes.Core.Blocks.Targets;
using DataPipes.Core.PipeTopology;

namespace Sandbox;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        using var source = new EnumerablePipeSource<int>([1, 2, 3], true);
        var runner = new PipeSourcePropagator<int>(source);
        var mapper = new MapperBlock<int, int>(x => x + 1);
        var sink = new ConsolePipeTarget<int>();

        runner.To(mapper).To(sink);

        var topology = new DefaultPipeTopologyExplorer().Explore(mapper);
        foreach (var pipeBlock in topology.Blocks)
        {
            var type = pipeBlock.GetType();
            if (type.Implements(typeof(IPipeTarget<>)))
                Console.WriteLine(type);
        }
    }

    public static bool Implements(this Type instanceType, Type interfaceType)
    {
        return instanceType
            .GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
    }
}
