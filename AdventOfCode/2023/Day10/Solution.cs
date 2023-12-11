namespace AdventOfCode.Y2023.Day10;

[ProblemName("Pipe Maze")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var grid = Parse(input);
        var start = FindStart(grid);

        var positions = GetPositionsInLoop(grid, start);

        return Math.Ceiling(positions.Count / 2d);
    }

    public object PartTwo(string input) 
    {
        var grid = Parse(input);
        var start = FindStart(grid);
        grid[start.Y][start.X] = '7';

        var positions = GetPositionsInLoop(grid, start);
        var scaledGrid = ScaleUp(grid, positions, 3);

        Bfs(scaledGrid);

        var scaledDown = ScaleDown(scaledGrid, 3);

        var correct = scaledDown
            .SelectMany(e => e)
            .Count(e => !e);

        return correct;
    }

    private static void Bfs(IReadOnlyList<bool[]> grid)
    {
        var height = grid.Count;
        var width = grid[0].Length;

        var queue = new Queue<Position>();
        queue.Enqueue(new Position(0, 0));
        queue.Enqueue(new Position(0, height - 1));
        queue.Enqueue(new Position(width - 1, 0));
        queue.Enqueue(new Position(width - 1, height - 1));

        while (queue.Count > 0)
        {
            var position = queue.Dequeue();

            if (position.X < 0 || position.X >= width || position.Y < 0 || position.Y >= height)
                continue;

            if (grid[position.Y][position.X])
                continue;

            grid[position.Y][position.X] = true;

            queue.Enqueue(new Position(position.X - 1, position.Y));
            queue.Enqueue(new Position(position.X + 1, position.Y));
            queue.Enqueue(new Position(position.X, position.Y - 1));
            queue.Enqueue(new Position(position.X, position.Y + 1));
        }
    }

    private static bool[][] ScaleDown(IReadOnlyList<bool[]> grid, int scaleFactor)
    {
        var scaledGrid = new bool[grid.Count / scaleFactor][];

        for (var i = 0; i < scaledGrid.Length; i++)
        {
            scaledGrid[i] = new bool[grid[i].Length / scaleFactor];
        }

        for (var y = 0; y < scaledGrid.Length; y++)
        {
            for (var x = 0; x < scaledGrid[0].Length; x++)
            {
                scaledGrid[y][x] = grid[y * scaleFactor][x * scaleFactor];
            }
        }

        return scaledGrid;
    }

    private static bool[][] ScaleUp(IReadOnlyList<char[]> grid, IReadOnlySet<Position> loopPositions, int scaleFactor)
    {
        var scaledGrid = new bool[grid.Count * scaleFactor][];

        for (var i = 0; i < scaledGrid.Length; i++)
        {
            scaledGrid[i] = new bool[grid[0].Length * scaleFactor];
        }

        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if(!loopPositions.Contains(new Position(x, y)))
                    continue;

                var scaledX = x * scaleFactor;
                var scaledY = y * scaleFactor;

                var pipe = CharToPipe(grid[y][x]);

                for (var i = 0; i < scaleFactor; i++)
                {
                    scaledGrid[scaledY + pipe.Ay * i][scaledX + pipe.Ax * i] = true;
                }

                for (var i = 0; i < scaleFactor; i++)
                {
                    scaledGrid[scaledY + pipe.By * i][scaledX + pipe.Bx * i] = true;
                }
            }
        }

        return scaledGrid;
    }

    private static HashSet<Position> GetPositionsInLoop(IReadOnlyList<char[]> grid, Position start)
    {
        // Find starting point
        var left = GetPositions(grid, start.X - 1, start.Y);
        var right = GetPositions(grid, start.X + 1, start.Y);
        var up = GetPositions(grid, start.X, start.Y - 1);
        var down = GetPositions(grid, start.X, start.Y + 1);

        var previous = start;
        Position position;
        if (left.Any(e => e == start))
            position = new Position(start.X - 1, start.Y);
        else if (right.Any(e => e == start))
            position = new Position(start.X + 1, start.Y);
        else if (up.Any(e => e == start))
            position = new Position(start.X, start.Y - 1);
        else if (down.Any(e => e == start))
            position = new Position(start.X, start.Y + 1);
        else
            throw new Exception("No starting point found");

        var positions = new HashSet<Position>
        {
            start,
            position
        };

        while (position != start)
        {
            var temp = position;
            position = GetPositions(grid, position.X, position.Y).First(e => e != previous);
            previous = temp;

            positions.Add(position);
        }

        return positions;
    }

    private static Position[] GetPositions(IReadOnlyList<char[]> grid, int x, int y)
    {
        if (grid[y][x] == 'S')
            return Array.Empty<Position>();

        var pipe = CharToPipe(grid[y][x]);

        return new Position[]
        {
            new(x + pipe.Ax, y + pipe.Ay),
            new(x + pipe.Bx, y + pipe.By)
        };
    }

    private static char[][] Parse(string input) => input.Split('\n').Select(s => s.ToCharArray()).ToArray();

    private static Position FindStart(IReadOnlyList<char[]> grid)
    {
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] == 'S')
                {
                    return new Position(x, y);
                }
            }
        }

        throw new Exception("No start found");
    }

    private static Pipe CharToPipe(char c)
    {
        return c switch
        {
            '|' => new Pipe(0, -1, 0, 1),
            '-' => new Pipe(-1, 0, 1, 0),
            'L' => new Pipe(0, -1, 1, 0),
            'J' => new Pipe(0, -1, -1, 0),
            '7' => new Pipe(-1, 0, 0, 1),
            'F' => new Pipe(1, 0, 0, 1),
            '.' => new Pipe(0, 0, 0, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
        };
    }

    private record struct Pipe(int Ax, int Ay, int Bx, int By);

    private record struct Position(int X, int Y);
}
