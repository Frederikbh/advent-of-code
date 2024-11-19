using System.Globalization;
using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day18;

[ProblemName("Lavaduct Lagoon")]
public partial class Solution : ISolver
{
    [GeneratedRegex(@"([LRUD]) (\d+) \(#(.+)\)")]
    private static partial Regex Parser();

    private static readonly Dictionary<char, Point> Directions = new()
    {
        { 'U', new Point(0, -1) },
        { 'D', new Point(0, 1) },
        { 'L', new Point(-1, 0) },
        { 'R', new Point(1, 0) },
    };

    public object PartOne(string input) 
    {
        var commands = ParseInput(input);

        var vertices = new List<Point>();
        var current = new Point(0, 0);
        var count = 0;

        foreach (var command in commands)
        {
            var direction = Directions[command.Direction];
            current = new Point(current.X + command.Distance * direction.X, current.Y + command.Distance * direction.Y);
            
            vertices.Add(current);
            count += command.Distance;
        }

        var area = ShoelaceArea(vertices);
        var total = PickTheorem(area, count);
        return total + count;
    }

    public object PartTwo(string input) 
    {
        var commands = ParseInput(input, true);

        var vertices = new List<Point>();
        var current = new Point(0, 0);
        var count = 0;

        foreach (var command in commands)
        {
            var direction = Directions[command.Direction];
            current = new Point(current.X + command.Distance * direction.X, current.Y + command.Distance * direction.Y);

            vertices.Add(current);
            count += command.Distance;
        }

        var area = ShoelaceArea(vertices);
        var total = PickTheorem(area, count);
        return total + count;
    }

    private static double PickTheorem(double area, int boundary)
    {
        return area - boundary / 2d + 1;
    }

    private static double ShoelaceArea(IReadOnlyList<Point> points)
    {
        var area = 0d;

        for (var i = 0; i < points.Count - 1; i++)
        {
            area += points[i].X * points[i + 1].Y - points[i + 1].X * points[i].Y;
        }

        return area / 2d;
    }

    private static List<Command> ParseInput(string input, bool useColor = false)
    {
        var regex = Parser();
        var lines = input.Split("\n");
        var commands = new List<Command>();

        foreach (var line in lines)
        {
            var match = regex.Match(line);
            var direction = match.Groups[1].Value[0];
            var distance = int.Parse(match.Groups[2].Value);
            var color = match.Groups[3].Value;

            if (useColor)
            {
                var hexDigit = color[..5];

                distance = int.Parse(hexDigit, NumberStyles.HexNumber);

                direction = color[5] switch
                {

                    '0' => 'R',
                    '1' => 'D',
                    '2' => 'L',
                    '3' => 'U',
                    _ => throw new Exception("Invalid direction")
                };
            }

            commands.Add(new Command(direction, distance));

        }

        return commands;
    }

    private record Command(char Direction, int Distance);
    private record Point(long X, long Y);
}
