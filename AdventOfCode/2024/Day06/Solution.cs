using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day06;

[ProblemName("Guard Gallivant")]
public class Solution : ISolver
{
    private static readonly Point[] s_directions = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];

    public object PartOne(string input)
    {
        var grid = ParseInput(input);
        var pos = FindStart(grid);

        return Walk(grid, pos)
            .Count;
    }

    public object PartTwo(string input)
    {
        var grid = ParseInput(input);
        var pos = FindStart(grid);

        var visited = Walk(grid, pos);

        var loops = 0;

        foreach (var visitedPoint in visited)
        {
            AddWall(grid, visitedPoint);

            if (Loops(grid, pos.X, pos.Y))
            {
                loops++;
            }

            RemoveWall(grid, visitedPoint);
        }

        return loops;
    }

    private static HashSet<Point> Walk(char[][] grid, Point pos)
    {
        var dir = 0;
        var width = grid[0].Length;
        var height = grid.Length;

        var visited = new HashSet<Point>();

        while (!IsOutOfBounds(width, height, pos.X, pos.Y))
        {
            var direction = s_directions[dir];
            var next = new Point(pos.X + direction.X, pos.Y + direction.Y);

            if (IsWall(grid, next.X, next.Y))
            {
                dir = (dir + 1) % 4;
                next = new Point(pos.X + s_directions[dir].X, pos.Y + s_directions[dir].Y);
            }

            visited.Add(pos);
            pos = next;
        }

        return visited;
    }

    private static bool Loops(char[][] grid, int x, int y)
    {
        var width = grid[0].Length;
        var height = grid.Length;
        var dir = 0;

        // We'll encode state as: state = (y * width + x)*4 + dir
        // Max states: width * height * 4
        var visitedStates = new bool[width * height * 4];

        while (!IsOutOfBounds(width, height, x, y))
        {
            var state = (y * width + x) * 4 + dir;

            if (visitedStates[state])
            {
                return true;
            }

            visitedStates[state] = true;

            var (dx, dy) = s_directions[dir];
            var nextX = x + dx;
            var nextY = y + dy;

            if (IsWall(grid, nextX, nextY))
            {
                dir = (dir + 1) % 4;

                continue;
            }

            x = nextX;
            y = nextY;
        }

        return false;
    }

    private static void AddWall(char[][] grid, Point wall) => grid[wall.Y][wall.X] = '#';

    private static void RemoveWall(char[][] grid, Point wall) => grid[wall.Y][wall.X] = '.';

    private static Point FindStart(char[][] grid)
    {
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == '^')
                {
                    return new Point(x, y);
                }
            }
        }

        return new Point(-1, -1);
    }

    private static bool IsWall(char[][] grid, int x, int y) =>
        !IsOutOfBounds(grid[0].Length, grid.Length, x, y) && grid[y][x] == '#';

    private static bool IsOutOfBounds(int width, int height, int x, int y) =>
        x < 0 || y < 0 || y >= height || x >= width;

    private static char[][] ParseInput(string input) =>
        input.Split("\n")
            .Select(x => x.ToCharArray())
            .ToArray();

    private record struct Point(int X, int Y);
}
