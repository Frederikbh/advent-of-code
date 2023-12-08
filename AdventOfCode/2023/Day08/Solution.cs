using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

using AngleSharp.Dom;

namespace AdventOfCode.Y2023.Day08;

[ProblemName("Haunted Wasteland")]
internal class Solution : ISolver 
{

    public object PartOne(string input)
    {
        var parsedInput = ParseInput(input);

        var node = "AAA";
        var steps = 0;

        while (node != "ZZZ")
        {
            var command = parsedInput.Commands[steps % parsedInput.Commands.Length];

            var left = parsedInput.Nodes[node].Left;
            var right = parsedInput.Nodes[node].Right;
            node = command switch
            {
                'L' => left,
                'R' => right,
                _ => throw new Exception("Unknown command")
            };
            steps++;
        }

        return steps;
    }

    public object PartTwo(string input) 
    {
        var parsedInput = ParseInput(input);

        var nodes = parsedInput.Nodes
            .Where(e => e.Key.EndsWith('A'))
            .ToDictionary(e => GetStepsToValidGhostEnd(parsedInput, e.Key))
            .ToList();

        var result = nodes.Aggregate(1L, (acc, e) => Lcm(acc, e.Key));

        return result;
    }

    private static long Gcd(long a, long b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }

        return a;
    }

    private static long Lcm(long a, long b)
    {
        return (a / Gcd(a, b)) * b;
    }

    private static long GetStepsToValidGhostEnd(Input input, string from)
    {
        var steps = 0;

        while (!from.EndsWith('Z'))
        {
            var command = input.Commands[steps % input.Commands.Length];
            var left = input.Nodes[from].Left;
            var right = input.Nodes[from].Right;
            from = command switch
            {
                'L' => left,
                'R' => right,
                _ => throw new Exception("Unknown command")
            };
            steps++;
        }

        return steps;
    }

    private static Input ParseInput(string input)
    {
        var regex = new Regex(@"(\w+) = \((\w+), (\w+)\)");
        var lines = input.Split("\n");
        var commands = lines[0];

        var nodes = new Dictionary<string, Node>();
        for (var i = 2; i < lines.Length; i++)
        {
            var match = regex.Match(lines[i]);
            var left = match.Groups[2].Value;
            var right = match.Groups[3].Value;
            nodes.Add(match.Groups[1].Value, new Node(left, right));
        }

        return new Input(commands, nodes);
    }

    private record Input(string Commands, Dictionary<string, Node> Nodes);

    private record Node(string Left, string Right);
}
