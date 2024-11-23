using System.Numerics;
using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day13;

[ProblemName("A Maze of Twisty Little Cubicles")]
public class Solution : ISolver
{
    // Cache for storing the results of IsOpenSpace to avoid redundant calculations
    private readonly Dictionary<(int x, int y), bool> _isOpenSpaceCache = new();

    public object PartOne(string input)
    {
        var favoriteNumber = int.Parse(input);
        var shortestPath = ShortestPath((1, 1), (31, 39), favoriteNumber);
        return shortestPath;
    }

    public object PartTwo(string input)
    {
        var favoriteNumber = int.Parse(input);
        var visited = new HashSet<(int x, int y)>();
        var queue = new Queue<(int x, int y, int steps)>();
        queue.Enqueue((1, 1, 0));

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();

            if (steps > 50)
            {
                // Return the number of unique positions visited within 50 steps
                return visited.Count;
            }

            if (!visited.Add((x, y)))
            {
                continue;
            }

            foreach (var neighbor in GetNeighbors(x, y))
            {
                if (IsOpenSpace(neighbor, favoriteNumber))
                {
                    queue.Enqueue((neighbor.x, neighbor.y, steps + 1));
                }
            }
        }

        return -1;
    }

    public static IEnumerable<(int x, int y)> GetNeighbors(int x, int y)
    {
        yield return (x, y - 1);
        yield return (x, y + 1);
        yield return (x - 1, y);
        yield return (x + 1, y);
    }

    public bool IsOpenSpace((int x, int y) point, int favoriteNumber)
    {
        if (_isOpenSpaceCache.TryGetValue(point, out var isOpen))
        {
            return isOpen;
        }

        var (xCord, yCord) = point;

        if (xCord < 0 || yCord < 0)
        {
            _isOpenSpaceCache[point] = false;
            return false;
        }

        var num = (long)xCord * xCord + 3L * xCord + 2L * xCord * yCord + yCord + (long)yCord * yCord + favoriteNumber;
        var bitCount = BitOperations.PopCount((ulong)num);

        isOpen = bitCount % 2 == 0;
        _isOpenSpaceCache[point] = isOpen;
        return isOpen;
    }

    public int ShortestPath((int x, int y) start, (int x, int y) end, int favoriteNumber)
    {
        var visited = new HashSet<(int x, int y)>();
        var queue = new Queue<(int x, int y, int steps)>();
        queue.Enqueue((start.x, start.y, 0));

        while (queue.Count > 0)
        {
            var (x, y, steps) = queue.Dequeue();

            if (x == end.x && y == end.y)
            {
                return steps;
            }

            if (!visited.Add((x, y)))
            {
                continue;
            }

            foreach (var neighbor in GetNeighbors(x, y))
            {
                if (IsOpenSpace(neighbor, favoriteNumber))
                {
                    queue.Enqueue((neighbor.x, neighbor.y, steps + 1));
                }
            }
        }

        return -1; // Path not found
    }
}
