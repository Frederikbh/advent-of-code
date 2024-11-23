using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2015.Day25;

[ProblemName("Let It Snow")]
public partial class Solution : ISolver
{
    public object PartOne(string input)
    {
        var match = InputRegex().Match(input);

        if (!match.Success)
        {
            throw new Exception("Invalid input");
        }

        var row = int.Parse(match.Groups[1].Value);
        var col = int.Parse(match.Groups[2].Value);

        return CalculateCode(row, col);
    }

    public object PartTwo(string input) => 0;

    private static long CalculateCode(int row, int col)
    {
        const long Multiplier = 252533;
        const long Divider = 33554393;

        var position = GetSequenceIndex(row, col);

        long code = 20151125;
        for (long i = 1; i < position; i++)
        {
            code = code * Multiplier % Divider;
        }

        return code;
    }

    private static long GetSequenceIndex(int row, int col)
    {
        // Calculate the position in the sequence using triangular numbers
        long diagonal = row + col - 1;
        return (diagonal * (diagonal - 1)) / 2 + col;
    }

    [GeneratedRegex(@"To continue, please consult the code grid in the manual.  Enter the code at row (\d+), column (\d+).")]
    public static partial Regex InputRegex();
}
