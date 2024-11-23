using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day18;

[ProblemName("Like a Rogue")]
public class Solution : ISolver
{
    public object PartOne(string input) => CountSafeTiles(input, 40);

    public object PartTwo(string input) => CountSafeTiles(input, 400000);

    private static int CountSafeTiles(string firstRow, int rows)
    {
        var length = firstRow.Length;
        var prevRow = new bool[length + 2];
        var curRow = new bool[length + 2];

        var safeTiles = 0;

        for (var i = 0; i < length; i++)
        {
            prevRow[i + 1] = firstRow[i] == '^';
            if (!prevRow[i + 1])
            {
                safeTiles++;
            }
        }

        for (var i = 1; i < rows; i++)
        {
            for (var j = 1; j <= length; j++)
            {
                var left = prevRow[j - 1];
                var center = prevRow[j];
                var right = prevRow[j + 1];
                curRow[j] = IsTrap(left, center, right);
                if (!curRow[j])
                {
                    safeTiles++;
                }
            }

            (prevRow, curRow) = (curRow, prevRow);
        }

        return safeTiles;
    }

    private static bool IsTrap(bool left, bool center, bool right) =>
        (left && center  && !right)
        || (center && right && !left)
        || (left && !center && !right)
        || (right && !center && !left);
}
