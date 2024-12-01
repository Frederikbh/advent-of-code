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

        var result = 0;

        for (var i = 0; i < list1.Length; i++)
        {
            result += Math.Abs(list1[i] - list2[i]);
        }

        return result;
    }

    public object PartTwo(string input)
    {
        var (list1, list2) = ParseInput(input);

        var lookup = new Dictionary<int, int>(list2.Length);

        foreach (var val in list2)
        {
            if (!lookup.TryAdd(val, 1))
            {
                lookup[val]++;
            }
        }

        var result = 0;

        foreach (var val in list1)
        {
            if (lookup.TryGetValue(val, out var count))
            {
                result += count * val;
            }
        }

        return result;
    }

    private static (int[], int[]) ParseInput(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var list1 = new int[lines.Length];
        var list2 = new int[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var numbers = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1[i] = int.Parse(numbers[0]);
            list2[i] = int.Parse(numbers[1]);
        }

        return (list1, list2);
    }
}
