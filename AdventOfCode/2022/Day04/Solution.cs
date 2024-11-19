using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2022.Day04;

[ProblemName("Camp Cleanup")]
internal partial class Solution : ISolver
{
    [GeneratedRegex(@"(\d+)-(\d+),(\d+)-(\d+)")]
    private static partial Regex MatchLine();

    public object? PartOne(string input)
    {
        var pairs = ParseInput(input);

        var count = pairs
            .Count(e => IsRangeInclusive(e.First, e.Second) || IsRangeInclusive(e.Second, e.First));

        return count;
    }

    public object? PartTwo(string input)
    {
        var pairs = ParseInput(input);

        var count = pairs
            .Count(e => IsRangeOverlapping(e.First, e.Second) || IsRangeOverlapping(e.Second, e.First));

        return count;
    }

    private static bool IsRangeOverlapping(Range first, Range second)
    {
        return first.Start.Value <= second.End.Value 
            && first.End.Value >= second.Start.Value;
    }

    private static bool IsRangeInclusive(Range first, Range second)
    {
        return first.Start.Value <= second.Start.Value 
            && first.End.Value >= second.End.Value;
    }

    private static List<Pair> ParseInput(string input)
    {
        var pairs = new List<Pair>();
        foreach (var line in input.Split('\n'))
        {
            var match = MatchLine().Match(line);
            var first = new Range(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            var second = new Range(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

            var pair = new Pair(first, second);
            pairs.Add(pair);
        }

        return pairs;
    }

    private record Pair(Range First, Range Second);
}
