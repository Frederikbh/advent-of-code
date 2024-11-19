using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day13;

[ProblemName("Point of Incidence")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var patterns = ParseInput(input);

        var sum = 0;
        foreach (var grid in patterns)
        {
            var horizontal = FindMirror(grid);
            var vertical = FindMirror(Rotate(grid));

            sum += vertical;
            sum += 100 * horizontal;
        }

        return sum;
    }

    private static int FindMirror(char[][] grid, int smudges = 0)
    {
        for (var i = 0; i < grid.Length - 1; i++)
        {
            var diffs = grid[i].Zip(grid[i + 1]).Count(e => e.First != e.Second);
            if (diffs > smudges)
                continue;

            // Check if mirror is valid
            var j = 1;
            while (i - j >= 0 && i + j + 1 < grid.Length)
            {
                diffs += grid[i - j].Zip(grid[i + j + 1]).Count(e => e.First != e.Second);

                if (diffs > smudges)
                    break;
                j++;
            }

            if (diffs == smudges)
                return i + 1;
        }

        return 0;
    }

    private static char[][] Rotate(char[][] grid)
    {
        var newGrid = new char[grid[0].Length][];

        for (var i = 0; i < grid[0].Length; i++)
        {
            newGrid[i] = new char[grid.Length];

            for (var j = 0; j < grid.Length; j++)
            {
                newGrid[i][j] = grid[j][grid[0].Length - i - 1];
            }
        }

        return newGrid.Reverse().ToArray();
    }

    public object PartTwo(string input) 
    {
        var patterns = ParseInput(input);

        var sum = 0;
        foreach (var grid in patterns)
        {
            var horizontal = FindMirror(grid, 1);
            var vertical = FindMirror(Rotate(grid), 1);

            sum += vertical;
            sum += 100 * horizontal;
        }

        return sum;
    }

    private static List<char[][]> ParseInput(string input)
    {
        var patterns = new List<char[][]>();

        var splitPatterns = input.Split("\n\n");

        foreach (var splitPattern in splitPatterns)
        {
            var lines = splitPattern.Split("\n");

            var grid = new char[lines.Length][];

            for (var i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }

            patterns.Add(grid);
        }

        return patterns;
    }
}
