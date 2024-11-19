using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day01;

[ProblemName("No Time for a Taxicab")]
public class Solution : ISolver
{
    private static readonly (int, int)[] s_directions =
    [
        (0, 1), // North
        (1, 0), // East
        (0, -1), // South
        (-1, 0) // West
    ];

    public object PartOne(string input)
    {
        var directions = ParseInput(input);

        return Traverse(directions, false);
    }

    public object PartTwo(string input)
    {
        var directions = ParseInput(input);

        return Traverse(directions, true);
    }

    private static int Traverse(List<(char, int)> directions, bool stopAtRevisit)
    {
        var x = 0;
        var y = 0;
        var direction = 0;

        var visited = new HashSet<(int, int)>();

        foreach (var (turn, distance) in directions)
        {
            direction = (direction + (turn == 'R' ? 1 : 3)) % 4;
            var (dx, dy) = s_directions[direction];

            for (var i = 0; i < distance; i++)
            {
                x += dx;
                y += dy;
                if (stopAtRevisit && !visited.Add((x, y)))
                {
                    return Math.Abs(x) + Math.Abs(y);
                }
            }
        }

        return Math.Abs(x) + Math.Abs(y);
    }

    public static List<(char, int)> ParseInput(string input) =>
        input.Split(", ")
            .Select(e => (e[0], int.Parse(e[1..])))
            .ToList();
}
