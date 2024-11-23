using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day03;

[ProblemName("Spiral Memory")]
public class Solution : ISolver
{
    public object PartOne(string input) => ManhattanDistance(int.Parse(input));

    public object PartTwo(string input)
    {
        var target = int.Parse(input);

        return FirstValueLargerThan(target);
    }

    private static int FirstValueLargerThan(int target)
    {
        int[][] directions =
        [
            [1, 0], // Right
            [0, 1], // Up
            [-1, 0], // Left
            [0, -1] // Down
        ];

        var grid = new Dictionary<(int x, int y), int> { [(0, 0)] = 1 };

        int x = 0,
            y = 0;
        var steps = 1;
        var dirIndex = 0;

        while (true)
        {
            for (var i = 0; i < 2; i++) // There are two legs for each step size
            {
                for (var j = 0; j < steps; j++)
                {
                    // Move to the next position
                    x += directions[dirIndex % 4][0];
                    y += directions[dirIndex % 4][1];

                    // Calculate the sum of all adjacent squares
                    var value = GetAdjacentSum(x, y, grid);
                    grid[(x, y)] = value;

                    // Check if we've found the value
                    if (value > target)
                    {
                        return value;
                    }
                }

                dirIndex++; // Change direction after each leg
            }

            steps++; // Increase the number of steps after completing two legs
        }
    }

    private static int GetAdjacentSum(int x, int y, Dictionary<(int x, int y), int> grid)
    {
        var sum = 0;

        // All adjacent positions (including diagonals)
        for (var dx = -1; dx <= 1; dx++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                {
                    continue; // Skip the current position
                }

                if (grid.TryGetValue((x + dx, y + dy), out var adjacentValue))
                {
                    sum += adjacentValue;
                }
            }
        }

        return sum;
    }

    private static int ManhattanDistance(int n)
    {
        if (n == 1)
        {
            return 0;
        }

        var layer = (int)Math.Ceiling((Math.Sqrt(n) - 1) / 2);
        var maxNumberInLayer = (2 * layer + 1) * (2 * layer + 1);
        var sideLength = 2 * layer;
        var stepsToReachMidpoint = Math.Abs(n - (maxNumberInLayer - layer));

        for (var i = 1; i < 4; i++)
        {
            var midpoint = maxNumberInLayer - layer - sideLength * i;
            var distance = Math.Abs(n - midpoint);

            if (distance < stepsToReachMidpoint)
            {
                stepsToReachMidpoint = distance;
            }
        }

        return layer + stepsToReachMidpoint;
    }
}
