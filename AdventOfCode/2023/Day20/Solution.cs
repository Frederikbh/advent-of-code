using System.Text.RegularExpressions;

using Signal = (string sender, string receiver, bool value);

namespace AdventOfCode.Y2023.Day20;

[ProblemName("Pulse Propagation")]
public class Solution : ISolver 
{
    public object PartOne(string input) 
    {
        var gates = ParseGates(input);
        var values = (
            from _ in Enumerable.Range(0, 1000)
            from signal in Trigger(gates)
            select signal.value
        ).ToArray();
        return values.Count(v => v) * values.Count(v => !v);
    }

    public object PartTwo(string input) 
    {
        var gates = ParseGates(input);
        var nand = gates["rx"].Inputs.Single();
        var branches = gates[nand].Inputs;
        return branches.Aggregate(1L, (m, branch) => m * LoopLength(input, branch));
    }

    private int LoopLength(string input, string output)
    {
        var gates = ParseGates(input);
        for (var i = 1; ; i++)
        {
            var signals = Trigger(gates);
            if (signals.Any(s => s.sender == output && s.value))
            {
                return i;
            }
        }
    }

    private IEnumerable<Signal> Trigger(Dictionary<string, Gate> gates)
    {
        var q = new Queue<Signal>();
        q.Enqueue(new Signal("button", "broadcaster", false));

        while (q.TryDequeue(out var signal))
        {
            yield return signal;

            var handler = gates[signal.receiver];
            foreach (var signalT in handler.Handle(signal))
            {
                q.Enqueue(signalT);
            }
        }
    }

    private Dictionary<string, Gate> ParseGates(string input)
    {
        input += "\nrx ->"; // an extra rule for rx with no output

        var descriptions =
            from line in input.Split('\n')
            let words = Regex.Matches(line, "\\w+").Select(m => m.Value).ToArray()
            select (kind: line[0], name: words.First(), outputs: words[1..]);

        string[] Inputs(string name) =>
            (from d in descriptions
             where d.outputs.Contains(name)
             select d.name).ToArray();

        return descriptions.ToDictionary(
            d => d.name,
            d => d.kind switch {
                '&' => NandGate(d.name, Inputs(d.name), d.outputs),
                '%' => FlipFlop(d.name, Inputs(d.name), d.outputs),
                _ => Repeater(d.name, Inputs(d.name), d.outputs)
            }
        );
    }

    private Gate NandGate(string name, string[] inputs, string[] outputs)
    {
        var state = inputs.ToDictionary(input => input, _ => false);

        return new Gate(inputs, (Signal signal) => {
            state[signal.sender] = signal.value;
            var value = !state.Values.All(b => b);
            return outputs.Select(o => new Signal(name, o, value));
        });
    }

    private Gate FlipFlop(string name, string[] inputs, string[] outputs)
    {
        var state = false;

        return new Gate(inputs, signal => {
            if (!signal.value)
            {
                state = !state;
                return outputs.Select(o => new Signal(name, o, state));
            }
            else
            {
                return [];
            }
        });
    }

    private Gate Repeater(string name, string[] inputs, string[] outputs)
    {
        return new Gate(inputs, s =>
            from o in outputs select new Signal(name, o, s.value)
        );
    }

    private record Gate(string[] Inputs, Func<Signal, IEnumerable<Signal>> Handle);
}
