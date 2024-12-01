using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day09;

[ProblemName("Stream Processing")]
public class Solution : ISolver
{
    public object PartOne(string input) =>
        ParseInput(input)
            .groupScore;

    public object PartTwo(string input) =>
        ParseInput(input)
            .garbageCount;

    private static (int groupScore, int garbageCount) ParseInput(string input)
    {
        var groupScore = 0;
        var garbageCount = 0;
        var depth = 0;
        var inGarbage = false;

        for (var i = 0; i < input.Length;)
        {
            var c = input[i];

            if (inGarbage)
            {
                if (c == '!')
                {
                    i += 2; // Skip '!' and the next character
                }
                else if (c == '>')
                {
                    inGarbage = false;
                    i++;
                }
                else
                {
                    garbageCount++;
                    i++;
                }
            }
            else
            {
                if (c == '<')
                {
                    inGarbage = true;
                    i++;
                }
                else if (c == '{')
                {
                    depth++;
                    i++;
                }
                else if (c == '}')
                {
                    groupScore += depth;
                    depth--;
                    i++;
                }
                else
                {
                    // Skip any other character outside garbage
                    i++;
                }
            }
        }

        return (groupScore, garbageCount);
    }
}
