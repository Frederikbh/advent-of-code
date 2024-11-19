using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day09;

[ProblemName("Mirage Maintenance")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var numbers = ParseInput(input);

        return numbers
            .Select(GetNextInSequence)
            .Sum();
    }

    public object PartTwo(string input) 
    {
        var numbers = ParseInput(input);

        return numbers
            .Select(GetPreviousInSequence)
            .Sum();
    }

    private static int GetNextInSequence(IReadOnlyList<int> numbers)
    {
        if (numbers.All(e => e == 0))
            return 0;

        var diffs = new List<int>();

        for (var i = 1; i < numbers.Count; i++)
        {
            diffs.Add(numbers[i] - numbers[i - 1]);
        }

        return numbers[^1] + GetNextInSequence(diffs);
    }

    private static int GetPreviousInSequence(IReadOnlyList<int> numbers)
    {
        if (numbers.All(e => e == 0))
            return 0;

        var diffs = new List<int>();

        for (var i = 1; i < numbers.Count; i++)
        {
            diffs.Add(numbers[i] - numbers[i - 1]);
        }

        return numbers[0] - GetPreviousInSequence(diffs);
    }

    private static List<List<int>> ParseInput(string input)
    {
        var lines = input.Split("\n");
        var result = new List<List<int>>();

        foreach (var line in lines)
        {
            var numbers = line.Split(" ").Select(int.Parse).ToList();
            result.Add(numbers);
        }
        return result;
    }
}
