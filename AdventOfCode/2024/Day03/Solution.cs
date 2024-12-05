using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day03;

[ProblemName("Mull It Over")]
public partial class Solution : ISolver
{
    public object PartOne(string input) =>
        MultiplyRegex()
            .Matches(input)
            .Aggregate(0, (a, b) => a + Multiply(b));

    public object PartTwo(string input)
    {
        var dos = DoRegex()
            .Matches(input)
            .Select(m => m.Index)
            .ToList();
        var donts = DontRegex()
            .Matches(input)
            .Select(m => m.Index)
            .ToList();

        var commands = MultiplyRegex()
            .Matches(input);

        var result = 0;

        foreach (Match command in commands)
        {
            var commandIndex = command.Index;
            var nearestDo = dos.LastOrDefault(d => d < commandIndex);
            var nearestDont = donts.LastOrDefault(d => d < commandIndex);

            if ((nearestDo == default && nearestDont == default) || nearestDo > nearestDont)
            {
                result += Multiply(command);
            }
        }

        return result;
    }

    private static int Multiply(Match match)
    {
        var a = int.Parse(match.Groups[1].Value);
        var b = int.Parse(match.Groups[2].Value);

        return a * b;
    }

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MultiplyRegex();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex(@"don't\(\)")]
    private static partial Regex DontRegex();
}
