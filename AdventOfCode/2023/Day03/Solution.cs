using System.Buffers;

namespace AdventOfCode.Y2023.Day03;

[ProblemName("Gear Ratios")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var grid = ParseInput(input);

        var sum = 0;
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (!char.IsNumber(grid[i][j]))
                    continue;

                var startOfNumber = j;
                while (j + 1 < grid[i].Length && char.IsNumber(grid[i][j + 1]))
                    j++;

                if(!IsNearSpecialChar(grid, i, startOfNumber, j))
                    continue;

                var number = int.Parse(grid[i][startOfNumber..(j + 1)]);
                sum += number;
            }
        }

        return sum;
    }

    public object PartTwo(string input) 
    {
        var grid = ParseInput(input);

        var gearConnections = new Dictionary<Coordinate, List<int>>();
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (!char.IsNumber(grid[i][j]))
                    continue;

                var startOfNumber = j;
                while (j + 1 < grid[i].Length && char.IsNumber(grid[i][j + 1]))
                    j++;

                var gearPositions = GetGearPositions(grid, i, startOfNumber, j);

                var number = int.Parse(grid[i][startOfNumber..(j + 1)]);
                foreach (var gearPosition in gearPositions)
                {
                    if(!gearConnections.ContainsKey(gearPosition))
                        gearConnections.Add(gearPosition, new List<int>());

                    gearConnections[gearPosition].Add(number);
                }
            }
        }

        var validGears = gearConnections.Where(e => e.Value.Count == 2);

        var sum = 0;
        foreach (var validGear in validGears)
        {
            sum += validGear.Value.Aggregate(1, (acc, val) => acc * val);
        }

        return sum;
    }

    private static bool IsNearSpecialChar(IReadOnlyList<char[]> grid, int y, int leftX, int rightX)
    {
        var specialChars = SearchValues.Create(new[] { '&', '*', '=', '/', '+', '@', '$', '#', '%', '-' });
        var upperLeft = (Math.Max(0, y - 1), Math.Max(0, leftX - 1));
        var lowerRight = (Math.Min(grid.Count - 1, y + 1), Math.Min(grid[y].Length - 1, rightX + 1));

        for (var i = upperLeft.Item1; i <= lowerRight.Item1; i++)
        {
            for (var j = upperLeft.Item2; j <= lowerRight.Item2; j++)
            {
                if (specialChars.Contains(grid[i][j]))
                    return true;
            }
        }

        return false;
    }

    private static List<Coordinate> GetGearPositions(IReadOnlyList<char[]> grid, int y, int leftX, int rightX)
    {
        var upperLeft = (Math.Max(0, y - 1), Math.Max(0, leftX - 1));
        var lowerRight = (Math.Min(grid.Count - 1, y + 1), Math.Min(grid[y].Length - 1, rightX + 1));

        var coords = new List<Coordinate>();
        for (var i = upperLeft.Item1; i <= lowerRight.Item1; i++)
        {
            for (var j = upperLeft.Item2; j <= lowerRight.Item2; j++)
            {
                if (grid[i][j] == '*')
                {
                    coords.Add(new Coordinate(j, i));
                }
            }
        }

        return coords;
    }

    private static char[][] ParseInput(string input)
    {
        return input.Split("\n").Select(line => line.ToCharArray()).ToArray();
    }

    private record Coordinate(int X, int Y);
}
