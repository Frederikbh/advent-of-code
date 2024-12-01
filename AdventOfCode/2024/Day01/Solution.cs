using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day01;

[ProblemName("Historian Hysteria")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var (list1, list2) = ParseInput(input);

        Array.Sort(list1);
        Array.Sort(list2);

        return list1.Zip(list2, (a, b) => Math.Abs(a - b))
            .Sum();
    }

    public object PartTwo(string input)
    {
        var (list1, list2) = ParseInput(input);

        var lookup = list2.CountBy(x => x)
            .ToDictionary();

        return list1.Where(lookup.ContainsKey)
            .Sum(val => lookup[val] * val);
    }

    private static (int[], int[]) ParseInput(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var list1 = new int[lines.Length];
        var list2 = new int[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var numbers = lines[i]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1[i] = int.Parse(numbers[0]);
            list2[i] = int.Parse(numbers[1]);
        }

        return (list1, list2);
    }
}
