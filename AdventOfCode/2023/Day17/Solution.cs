using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day17;

[ProblemName("Clumsy Crucible")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var map = input.Split('\n').Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        var queue = new PriorityQueue<Path, int>();
        var visited = new HashSet<string>();

        queue.Enqueue(new Path(new(0, 0), Direction.Right, 0), 0);

        var totalHeat = 0;

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();

            if (path.Position.Row == map.Length - 1 && path.Position.Col == map[0].Length - 1)
            {
                totalHeat = path.Heat;
                break;
            }

            if (path.StraightLineLength < 3)
            {
                TryMove(map, visited, queue, path, path.Direction);
            }

            TryMove(map, visited, queue, path, path.Direction.TurnLeft());
            TryMove(map, visited, queue, path, path.Direction.TurnRight());
        }

        return totalHeat;
    }

    public object PartTwo(string input)
    {
        var map = input.Split('\n').Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        var queue = new PriorityQueue<Path, int>();
        var visited = new HashSet<string>();

        queue.Enqueue(new Path(new Position(0, 0), Direction.Right, 0), 0);
        queue.Enqueue(new Path(new Position(0, 0), Direction.Down, 0), 0);
        var totalHeat = 0;
        visited.Clear();

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            if (path.Position.Row == map.Length - 1 && path.Position.Col == map[0].Length - 1 && path.StraightLineLength >= 4)
            {
                totalHeat = path.Heat;
                break;
            }

            if (path.StraightLineLength < 10)
            {
                TryMove(map, visited, queue, path, path.Direction);
            }

            if (path.StraightLineLength >= 4)
            {
                TryMove(map, visited, queue, path, path.Direction.TurnLeft());
                TryMove(map, visited, queue, path, path.Direction.TurnRight());
            }
        }

        return totalHeat;
    }

    private static void TryMove(int[][] map, HashSet<string> visited, PriorityQueue<Path, int> queue, Path path, Direction direction)
    {
        var candidate = new Path(path.Position.Move(direction), direction, direction == path.Direction ? path.StraightLineLength + 1 : 1);

        if (candidate.Position.Row < 0 || candidate.Position.Row >= map.Length ||
            candidate.Position.Col < 0 || candidate.Position.Col >= map[0].Length)
        {
            return;
        }

        var key = $"{candidate.Position.Row},{candidate.Position.Col},{candidate.Direction.Row},{candidate.Direction.Col},{candidate.StraightLineLength}";
        if (!visited.Add(key))
        {
            return;
        }

        candidate.Heat = path.Heat + map[candidate.Position.Row][candidate.Position.Col];
        queue.Enqueue(candidate, candidate.Heat);
    }

    private record Path(Position Position, Direction Direction, int StraightLineLength)
    {
        public int Heat { get; set; }
    }

    private record Direction(int Row, int Col)
    {

        public Direction TurnLeft()
        {
            return new Direction(-Col, Row);
        }

        public Direction TurnRight()
        {
            return new Direction(Col, -Row);
        }

        public static Direction Up = new(-1, 0);
        public static Direction Down = new(1, 0);
        public static Direction Left = new(0, -1);
        public static Direction Right = new(0, 1);
    }

    private record Position(int Row, int Col)
    {

        public Position Move(Direction dir)
        {
            return new Position(Row + dir.Row, Col + dir.Col);
        }
    }
}
