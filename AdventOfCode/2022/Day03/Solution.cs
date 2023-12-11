namespace AdventOfCode.Y2022.Day03;

[ProblemName("Rucksack Reorganization")]
public class Solution : ISolver {

    public object? PartOne(string input)
    {
        var lines = input.Split('\n');

        var sum = lines.Select(SplitStringInHalf)
            .Sum(GetIntersectingPriority);

        return sum;
    }

    public object? PartTwo(string input)
    {
        var lines = input.Split('\n');

        var sum = lines
            .Chunk(3)
            .Sum(GetIntersectingPriority);

        return sum;
    }

    private static string[] SplitStringInHalf(string input)
    {
        var mid = input.Length / 2;

        return new[]
        {
            input[..mid],
            input[mid..]
        };
    }

    private static int GetIntersectingPriority(params string[] input)
    {
        return input
            .Select(e => e.AsEnumerable())
            .Aggregate((a, b) => a.Intersect(b))
            .Select(GetPriority)
            .Sum();
    }

    private static int GetPriority(char c)
    {
        return char.IsLower(c) switch
        {
            true => c - 'a' + 1,
            false => c - 'A' + 27
        };
    }
}
