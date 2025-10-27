using DataPipes.Core.PipeTopology;

namespace Sandbox;

public class ConsolePipeTopologyExporter
{
    public void Export(PipeTopology pipeTopology)
    {
        var printed = new HashSet<int>();
        foreach (var entryBlockIndex in pipeTopology.EntryBlockIndices.Order())
            Print(pipeTopology, printed, entryBlockIndex, 0);
    }

    private static void Print(PipeTopology pipeTopology, HashSet<int> printed, int index, int level)
    {
        var prefix = new string(' ', level * 2);
        if (!printed.Add(index))
        {
            var block = pipeTopology.Blocks[index].Meta.Name;
            Console.WriteLine(prefix + $"{block} ->|");
            return;
        }

        var blockName = pipeTopology.Blocks[index].Meta.Name;
        Console.WriteLine(prefix + blockName);

        if (!pipeTopology.BlockLinks.TryGetValue(index, out var link))
            return;

        foreach (var blockIndex in link.LinkedBlockIndices)
            Print(pipeTopology, printed, blockIndex, level + 1);
    }
}