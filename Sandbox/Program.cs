using DataPipes.Core;
using DataPipes.Core.Blocks.Sources;
using DataPipes.Core.Blocks.Targets;

namespace Sandbox;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        using var source = new EnumerablePipeSource<int>([1, 2, 3], true);
        var runner = new PipeSourcePropagator<int>(source);
        var sink1 = new ConsolePipeTarget<string>();
        var sink2 = new ConsolePipeTarget<string>();

        runner
            .To(new MapperBlock<int, string>(x => x.ToString()))
            .BranchInParallel(Environment.ProcessorCount)
            .AddLinkTo(new MapperBlock<string, string>(async x =>
            {
                if (Random.Shared.Next() % 2 == 0)
                    await Task.Delay(100);
                return "> " + x;
            }).To(sink1))
            .AddLinkTo(new MapperBlock<string, string>(x => ": " + x).To(sink2));

        var pipeTopology = new PipeTopologyExplorer().Explore(runner);
        foreach (var link in pipeTopology.Blocks)
            Console.WriteLine(link.Meta.Name);

        Console.WriteLine();
        await runner.Initialize(CancellationToken.None);
        await runner.Run(CancellationToken.None);
    }
}