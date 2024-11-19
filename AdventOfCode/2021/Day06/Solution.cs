using AdventOfCode.Lib;

namespace AdventOfCode._2021.Day06;

[ProblemName("Lanternfish")]
public class Solution : ISolver
{
    public object PartOne(string input) => Simulate(ParseInput(input), 80);

    public object PartTwo(string input) => Simulate(ParseInput(input), 256);

    private static long Simulate(long[] pond, int days)
    {
        // we will model a circular shift register, with an additional feedback:
        //       0123456           78 
        //   ┌──[.......]─<─(+)───[..]──┐
        //   └──────>────────┴─────>────┘
        //     reproduction     newborn

        for (var i = 0; i < days; i++)
        {
            pond[(i + 7) % 9] += pond[i % 9];
        }

        return pond.Sum();
    }

    private static long[] ParseInput(string input)
    {
        var pond = new long[9];

        foreach (var c in input.Split(','))
        {
            pond[int.Parse(c)]++;
        }

        return pond;
    }
}
