using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day02;

[ProblemName("Cube Conundrum")]
internal partial class Solution : ISolver
{
    [GeneratedRegex(@"(?<g>Game) (?<d>\d+)|(?<d>\d+) (?<g>green|red|blue)")]
    private static partial Regex GetInputRegex();

    private readonly Regex _inputRegex = GetInputRegex();

    public object PartOne(string input) 
    {
        var lines = input.Split("\n");

        return lines.Select(GetMaxValues)
            .Where(max => max["green"] <= 13 && max["red"] <= 12 && max["blue"] <= 14)
            .Sum(max => max["Game"]);
    }

    public object PartTwo(string input) 
    {
        var lines = input.Split("\n");

        return lines.Select(GetMaxValues)
            .Select(max => max["green"] * max["red"] * max["blue"])
            .Sum();
    }

    private Dictionary<string, int> GetMaxValues( string line)
    {
        var max = new Dictionary<string, int>();
        var matches = _inputRegex.Matches(line);

        foreach (Match match in matches)
        {
            var group = match.Groups["g"].Value;
            var val = int.Parse(match.Groups["d"].Value);
            max[group] = Math.Max(max.GetValueOrDefault(group), val);
        }

        return max;
    }
}
