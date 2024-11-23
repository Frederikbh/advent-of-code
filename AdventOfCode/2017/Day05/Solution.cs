using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day05;

[ProblemName("A Maze of Twisty Trampolines, All Alike")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var instructions = input.Split('\n')
            .Select(int.Parse)
            .ToArray();

        var i = 0;
        var steps = 0;

        while (i < instructions.Length)
        {
            i += instructions[i]++;
            steps++;
        }

        return steps;
    }

    public object PartTwo(string input)
    {
        var instructions = input.Split('\n')
            .Select(int.Parse)
            .ToArray();

        var i = 0;
        var steps = 0;

        while (i < instructions.Length)
        {
            var prev = i;
            i += instructions[i];

            instructions[prev] += instructions[prev] >= 3
                ? -1
                : 1;
            steps++;
        }

        return steps;
    }
}
