using AdventOfCode.Lib;

namespace AdventOfCode._2021.Day01;

[ProblemName("Sonar Sweep")]
public class Solution : ISolver 
{
    public object PartOne(string input) 
    {
        var depths = Parse(input);

        return depths[1..]
            .Zip(depths)
            .Count(e => e.First > e.Second);
    }

    public object PartTwo(string input) 
    {
        var depths = Parse(input);

        return depths[3..]
            .Zip(depths)
            .Count(e => e.First > e.Second);
    }

    private static int[] Parse(string input) => input.Split('\n').Select(int.Parse).ToArray();
}
