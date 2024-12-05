using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day04;

[ProblemName("Ceres Search")]
public class Solution : ISolver
{
    private static readonly List<(int modX, int modY)> s_directions =
    [
        (1, 0),
        (0, 1),
        (-1, 0),
        (0, -1),
        (1, 1),
        (-1, 1),
        (-1, -1),
        (1, -1)
    ];

    private const string Xmas = "XMAS";

    public object PartOne(string input)
    {
        var grid = ParseInput(input);
        var count = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] != 'X')
                {
                    continue;
                }

                count += s_directions.Count(direction => IsXmas(grid, y, x, 0, direction));
            }
        }

        return count;
    }

    public object PartTwo(string input)
    {
        var grid = ParseInput(input);
        var count = 0;

        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] != 'A')
                {
                    continue;
                }

                if (IsXmasCross(grid, y, x))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool IsXmasCross(char[][] grid, int y, int x)
    {
        if (!IsInBounds(grid, y - 1, x - 1) || !IsInBounds(grid, y + 1, x + 1))
        {
            return false;
        }

        var isLeftMas = (grid[y - 1][x - 1] == 'M' && grid[y + 1][x + 1] == 'S') ||
            (grid[y - 1][x - 1] == 'S' && grid[y + 1][x + 1] == 'M');
        var isRightMas = (grid[y - 1][x + 1] == 'M' && grid[y + 1][x - 1] == 'S') ||
            (grid[y - 1][x + 1] == 'S' && grid[y + 1][x - 1] == 'M');

        return isLeftMas && isRightMas;
    }

    private static bool IsXmas(char[][] grid, int y, int x, int charPos, (int modY, int modX) mod)
    {
        if (charPos > 3)
        {
            return true;
        }

        if (!IsInBounds(grid, y, x))
        {
            return false;
        }

        if (grid[y][x] != Xmas[charPos])
        {
            return false;
        }

        return IsXmas(grid, y + mod.modY, x + mod.modX, charPos + 1, mod);
    }

    private static bool IsInBounds(char[][] grid, int y, int x) =>
        y >= 0 && y < grid.Length && x >= 0 && x < grid[y].Length;

    private static char[][] ParseInput(string input) =>
        input.Split('\n')
            .Select(line => line.ToCharArray())
            .ToArray();
}
