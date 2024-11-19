using AdventOfCode.Lib;

namespace AdventOfCode._2022.Day01;

[ProblemName("Calorie Counting")]
public class Solution : ISolver {

    public object PartOne(string input)
    {
        var calories = CountElvesCalories(input);

        return calories.Max();
    }

    public object PartTwo(string input)
    {
        var calories = CountElvesCalories(input);

        return calories.OrderDescending()
            .Take(3)
            .Sum();
    }

    public IEnumerable<int> CountElvesCalories(string input)
    {
        var lines = input.Split('\n');

        var cur = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                yield return cur;
                cur = 0;
                continue;
            }

            cur += int.Parse(line);
        }
    }
}
