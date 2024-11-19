using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day21;

[ProblemName("Step Counter")]
public class Solution : ISolver 
{
    public object PartOne(string input)
    {
        var lines = input.Split('\n');

        var startPosition = lines.Length / 2; // 65
        var start = new Position(startPosition, startPosition);
        var visited = new HashSet<Position> { start };

        for (var steps = 0; steps < 64; steps++) // First run is 65 steps, the rest are 131 each.
        {
            var nextOpen = new HashSet<Position>();
            foreach (var position in visited)
            {
                foreach (var dir in new[] { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT })
                {
                    var dest = position.Move(dir);
                    if (lines[Modulo(dest.Row)][Modulo(dest.Col)] != '#')
                    {
                        nextOpen.Add(dest);
                    }
                }
            }

            visited = nextOpen;

        }

        return visited.Count;
    }

    public object PartTwo(string input)
    {
        var lines = input.Split('\n');
        const long TotalSteps = 26501365L;

        var sequenceCounts = new List<(int X, int Y)>();
        var startPosition = lines.Length / 2; // 65
        var start = new Position(startPosition, startPosition);
        var visited = new HashSet<Position> { start };
        var steps = 0;

        for (var run = 0; run < 3; run++)
        {
            for (; steps < run * 131 + 65; steps++) // First run is 65 steps, the rest are 131 each.
            {
                var nextOpen = new HashSet<Position>();
                foreach (var position in visited)
                {
                    foreach (var dir in new[] { Direction.UP, Direction.DOWN, Direction.LEFT, Direction.RIGHT })
                    {
                        var dest = position.Move(dir);
                        if (lines[Modulo(dest.Row)][Modulo(dest.Col)] != '#')
                        {
                            nextOpen.Add(dest);
                        }
                    }
                }

                visited = nextOpen;
            }
            sequenceCounts.Add((steps, visited.Count));
        }

        // Lagrange interpolation
        double result = 0;

        for (var i = 0; i < 3; i++)
        {
            // Compute individual terms of formula
            double term = sequenceCounts[i].Y;

            for (var j = 0; j < 3; j++)
            {
                if (j != i)
                {
                    term = term * (TotalSteps - sequenceCounts[j].X) / (sequenceCounts[i].X - sequenceCounts[j].X);
                }
            }

            // Add current term to result
            result += term;
        }

        return result;
    }

    private static int Modulo(int number)
    {
        return ((number % 131) + 131) % 131;
    }

    private record struct Direction(int Row, int Col)
    {
        public static readonly Direction UP = new(-1, 0);
        public static readonly Direction DOWN = new(1, 0);
        public static readonly Direction LEFT = new(0, -1);
        public static readonly Direction RIGHT = new(0, 1);
    }

    private readonly record struct Position(int Row, int Col)
    {
        public Position Move(Direction dir)
        {
            return new Position(Row + dir.Row, Col + dir.Col);
        }
    }
}
