using System.Drawing;

using AdventOfCode.Lib;

namespace AdventOfCode._2021.Day05;

[ProblemName("Hydrothermal Venture")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var lines = ParseInput(input);

        return CreateGrid(lines, true).Values.Count(e => e > 1);
    }

    public object PartTwo(string input)
    {
        var lines = ParseInput(input);

        return CreateGrid(lines, false).Values.Count(e => e > 1);
    }

    private static Grid CreateGrid(List<Line> lines, bool skipDiagonal)
    {
        var grid = new Grid();

        foreach (var line in lines)
        {
            if (skipDiagonal && line.Start.X != line.End.X && line.Start.Y != line.End.Y)
            {
                continue;
            }

            var xStep = Math.Sign(line.End.X - line.Start.X);
            var yStep = Math.Sign(line.End.Y - line.Start.Y);

            var position = line.Start;

            while (position != line.End)
            {
                grid[position]++;
                position = new Point(position.X + xStep, position.Y + yStep);
            }

            grid[line.End]++;
        }

        return grid;
    }

    private static List<Line> ParseInput(string input) =>
        input.Split('\n')
            .Select(line => line.Split(" -> "))
            .Select(points => new Line(ParsePoint(points[0]), ParsePoint(points[1])))
            .ToList();

    private static Point ParsePoint(string point)
    {
        var parts = point.Split(',');

        return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
    }
}

internal record Line(Point Start, Point End);

internal record Grid
{
    private readonly Dictionary<Point, int> _grid = new();

    public int this[Point point]
    {
        get => _grid.GetValueOrDefault(point, 0);
        set => _grid[point] = value;
    }

    public List<int> Values => _grid.Values.ToList();
}
