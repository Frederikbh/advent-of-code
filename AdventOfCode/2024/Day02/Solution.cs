using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day02;

[ProblemName("Red-Nosed Reports")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var reports = ParseInput(input);
        var safeReports = reports.Count(IsSafeReport);

        return safeReports;
    }

    public object PartTwo(string input)
    {
        var reports = ParseInput(input);
        var safeReports = reports.Count(e => IsSafeReport2(e));

        return safeReports;
    }

    private static bool IsSafeReport2(Span<int> report)
    {
        var dampenerReport = new int[report.Length - 1];

        for (var dampener = 0; dampener < report.Length; dampener++)
        {
            if (dampener > 0)
            {
                report[..dampener].CopyTo(dampenerReport);
            }
            if (dampener < report.Length - 1)
            {
                report[(dampener + 1)..].CopyTo(dampenerReport.AsSpan()[dampener..]);
            }

            var validReport = IsSafeReport(dampenerReport);

            if (validReport)
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsSafeReport(int[] report)
    {
        if (report.Length < 2)
        {
            return false;
        }

        var reportIncreasing = report[1] - report[0] > 0;

        for (var i = 0; i < report.Length - 1; i++)
        {
            var diff = report[i + 1] - report[i];

            if (diff == 0 || Math.Abs(diff) > 3 || diff > 0 != reportIncreasing)
            {
               return false;
            }
        }

        return true;
    }

    private static List<int[]> ParseInput(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var result = new List<int[]>();

        foreach (var line in lines)
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            result.Add(numbers);
        }

        return result;
    }
}
