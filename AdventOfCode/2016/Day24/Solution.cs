using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day24;

[ProblemName("Air Duct Spelunking")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var map = new Map(input);

        var permutations = Permutations(
            Enumerable.Range(1, map.PointsOfInterest.Length - 1)
                .ToArray());

        var minDistance = int.MaxValue;
        foreach (var permutation in permutations)
        {
            permutation.Insert(0, 0);

            var distance = 0;

            for (var i = 0; i < permutation.Count - 1; i++)
            {
                distance += map.ShortestPath(
                    map.PointsOfInterest[permutation[i]],
                    map.PointsOfInterest[permutation[i + 1]]);
            }

            minDistance = Math.Min(minDistance, distance);
        }

        return minDistance;
    }

    public object PartTwo(string input)
    {
        var map = new Map(input);

        var permutations = Permutations(
            Enumerable.Range(1, map.PointsOfInterest.Length - 1)
                .ToArray());

        var minDistance = int.MaxValue;
        foreach (var permutation in permutations)
        {
            permutation.Insert(0, 0);
            permutation.Add(0);

            var distance = 0;

            for (var i = 0; i < permutation.Count - 1; i++)
            {
                distance += map.ShortestPath(
                    map.PointsOfInterest[permutation[i]],
                    map.PointsOfInterest[permutation[i + 1]]);
            }

            minDistance = Math.Min(minDistance, distance);
        }

        return minDistance;
    }

    private static IEnumerable<List<T>> Permutations<T>(T[] rgt)
    {
        return PermutationsRec(0);

        IEnumerable<List<T>> PermutationsRec(int i)
        {
            if (i == rgt.Length)
            {
                yield return rgt.ToList();
            }

            for (var j = i; j < rgt.Length; j++)
            {
                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);

                foreach (var perm in PermutationsRec(i + 1))
                {
                    yield return perm;
                }

                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
            }
        }
    }
}

internal class Map
{
    private readonly string[] _map;
    public readonly Point[] PointsOfInterest;

    private readonly int _cols;
    private readonly int _rows;

    private readonly Dictionary<(Point From, Point To), int> _distances = new();

    private readonly int[] _dx = [0, 1, 0, -1];
    private readonly int[] _dy = [-1, 0, 1, 0];

    public Map(string input)
    {
        _map = input.Split('\n');
        _cols = _map[0].Length;
        _rows = _map.Length;

        PointsOfInterest = new Point[10];
        var pointOfInterestCount = 0;

        for (var i = 0; i < _rows; i++)
        {
            var row = _map[i]
                .AsSpan();

            for (var j = 0; j < _cols; j++)
            {
                if (int.TryParse(row[j..(j + 1)], out var pointOfInterest))
                {
                    PointsOfInterest[pointOfInterest] = new Point(i, j);
                    pointOfInterestCount++;
                }
            }
        }

        PointsOfInterest = PointsOfInterest[..pointOfInterestCount];
    }

    public int ShortestPath(Point from, Point to)
    {
        var key = (from, to);

        if (!_distances.ContainsKey(key))
        {
            var queue = new Queue<(Point Point, int Distance)>();
            queue.Enqueue((from, 0));
            var seen = new HashSet<Point> { from };

            while (queue.Count > 0)
            {
                var (point, distance) = queue.Dequeue();

                if (point == to)
                {
                    _distances[key] = distance;

                    break;
                }

                for (var i = 0; i < _dx.Length; i++)
                {
                    var next = new Point(point.Y + _dy[i], point.X + _dx[i]);
                    var withinBounds = next.Y >= 0 && next.Y < _rows && next.X >= 0 && next.X < _cols;

                    if (withinBounds && _map[next.Y][next.X] != '#' && seen.Add(next))
                    {
                        queue.Enqueue((next, distance + 1));
                    }
                }
            }
        }

        return _distances[key];
    }
}

internal record struct Point(int Y, int X);
