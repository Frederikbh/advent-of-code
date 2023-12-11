using System.Text.RegularExpressions;

using AngleSharp.Text;

namespace AdventOfCode.Y2022.Day05;

[ProblemName("Supply Stacks")]
public class Solution : ISolver 
{

    public object PartOne(string input)
    {
        var parsedInput = ParseInput(input);

        foreach (var command in parsedInput.Commands)
        {
            for (var i = 0; i < command.Quantity; i++)
            {
                var c = parsedInput.Stacks[command.Source].Pop();
                parsedInput.Stacks[command.Destination].Push(c);
            }
        }

        return string.Concat(parsedInput.Stacks.Select(e => e.Value.Pop()));
    }

    public object PartTwo(string input) 
    {
        var parsedInput = ParseInput(input);

        foreach (var command in parsedInput.Commands)
        {
            var temp = new Stack<char>();
            for (var i = 0; i < command.Quantity; i++)
            {
                var c = parsedInput.Stacks[command.Source].Pop();
                temp.Push(c);
            }

            foreach (var c in temp)
            {
                parsedInput.Stacks[command.Destination].Push(c);
            }
        }

        return string.Concat(parsedInput.Stacks.Select(e => e.Value.Pop()));
    }

    private static Input ParseInput(string input)
    {
        var lines = input.Split('\n');

        var stacks = new Dictionary<int, Stack<char>>
        {
            { 1, new Stack<char>() },
            { 2, new Stack<char>() },
            { 3, new Stack<char>() },
            { 4, new Stack<char>() },
            { 5, new Stack<char>() },
            { 6, new Stack<char>() },
            { 7, new Stack<char>() },
            { 8, new Stack<char>() },
            { 9, new Stack<char>() }
        };

        for (var i = 7; i >= 0; i--)
        {
            var line = lines[i];
            for (var j = 1; j < 10; j++)
            {
                var c = line[4 * j - 3];
                if(c.IsLetter())
                    stacks[j].Push(c);
            }
        }

        var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
        var commands = new List<Command>();
        for (var i = 10; i < lines.Length; i++)
        {
            var match = regex.Match(lines[i]);
            var quantity = int.Parse(match.Groups[1].Value);
            var source = int.Parse(match.Groups[2].Value);
            var destination = int.Parse(match.Groups[3].Value);
            commands.Add(new Command(quantity, source, destination));
        }

        return new Input(stacks, commands);
    }

    private record Input(Dictionary<int, Stack<char>> Stacks, List<Command> Commands);

    private record Command(int Quantity, int Source, int Destination);
}
