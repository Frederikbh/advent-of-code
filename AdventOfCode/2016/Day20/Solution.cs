using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day20;

[ProblemName("Firewall Rules")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var ranges = ParseInput(input);
        var i = 0L;

        foreach (var range in ranges)
        {
            if (i < range.Min)
            {
                break;
            }
            else if (range.Min <= i && i <= range.Max)
            {
                i = range.Max + 1;
            }
        }

        return i;
    }

    public object PartTwo(string input)
    {
        const long UpperLimit = 4294967295;

        var ranges = ParseInput(input);
        var i = 0L;
        var validIps = 0L;

        foreach (var range in ranges)
        {
            if (i < range.Min)
            {
                validIps += range.Min - i;
                i = range.Max + 1;
            }
            else if (range.Min <= i && i <= range.Max)
            {
                i = range.Max + 1;
            }
        }

        if (i < UpperLimit)
        {
            validIps += UpperLimit - i;
        }

        return validIps;
    }

    private static List<Range> ParseInput(string input) =>
        input.Split('\n')
            .Select(
                e =>
                {
                    var split = e.Split('-');

                    return new Range(long.Parse(split[0]), long.Parse(split[1]));
                })
            .OrderBy(e => e.Min)
            .ToList();
}

internal record struct Range(long Min, long Max);
