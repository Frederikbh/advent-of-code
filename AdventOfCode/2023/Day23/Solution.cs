using System.Numerics;

using AdventOfCode.Lib;

using Map = System.Collections.Generic.Dictionary<System.Numerics.Complex, char>;
using Node = long;

namespace AdventOfCode._2023.Day23;

[ProblemName("A Long Walk")]
public class Solution : ISolver 
{
    private static readonly Complex Up = -Complex.ImaginaryOne;
    private static readonly Complex Down = Complex.ImaginaryOne;
    private static readonly Complex Left = -1;
    private static readonly Complex Right = 1;
    private static readonly Complex[] Dirs = [Up, Down, Left, Right];

    private Dictionary<char, Complex[]> _exits = new()
    {
        ['<'] = [Left],
        ['>'] = [Right],
        ['^'] = [Up],
        ['v'] = [Down],
        ['.'] = Dirs,
        ['#'] = []
    };

    public object PartOne(string input) 
    {
        return Solve(input);
    }

    public object PartTwo(string input)
    {
        return Solve(RemoveSlopes(input));
    }

    private static string RemoveSlopes(string st) =>
        string.Join("", st.Select(ch => ">v<^".Contains(ch) ? '.' : ch));

    private int Solve(string input)
    {
        var (nodes, edges) = MakeGraph(input);
        var (start, goal) = (nodes.First(), nodes.Last());

        // Dynamic programming using a cache, 'visited' is a bitset of 'nodes'.
        var cache = new Dictionary<(Node, long), int>();

        return LongestPath(start, 0);

        int LongestPath(Node node, Node visited)
        {
            if (node == goal)
            {
                return 0;
            }
            if ((visited & node) != 0)
            {
                return int.MinValue; // small enough to represent '-infinity'
            }
            var key = (node, visited);
            if (!cache.TryGetValue(key, out var value))
            {
                value = edges
                    .Where(e => e.Start == node)
                    .Select(e => e.Distance + LongestPath(e.End, visited | node))
                    .Max();
                cache[key] = value;
            }
            return value;
        }
    }

    private (Node[], Edge[]) MakeGraph(string input)
    {
        var map = ParseMap(input);

        // row-major order: 'entry' node comes first and 'exit' is last
        var nodePos = (
            from pos in map.Keys
            orderby pos.Imaginary, pos.Real
            where IsFree(map, pos) && !IsRoad(map, pos)
            select pos
        ).ToArray();

        var nodes = (
            from i in Enumerable.Range(0, nodePos.Length) select 1L << i
        ).ToArray();

        var edges = (
            from i in Enumerable.Range(0, nodePos.Length)
            from j in Enumerable.Range(0, nodePos.Length)
            where i != j
            let distance = Distance(map, nodePos[i], nodePos[j])
            where distance > 0
            select new Edge(nodes[i], nodes[j], distance)
        ).ToArray();

        return (nodes, edges);
    }

    // Length of the road between two crossroads; -1 if not neighbours
    private int Distance(Map map, Complex crossroadA, Complex crossroadB)
    {
        var q = new Queue<(Complex, int)>();
        q.Enqueue((crossroadA, 0));

        var visited = new HashSet<Complex> { crossroadA };
        while (q.Count != 0)
        {
            var (pos, dist) = q.Dequeue();
            foreach (var dir in _exits[map[pos]])
            {
                var posT = pos + dir;
                if (posT == crossroadB)
                {
                    return dist + 1;
                }
                else if (IsRoad(map, posT) && visited.Add(posT))
                {
                    q.Enqueue((posT, dist + 1));
                }
            }
        }
        return -1;
    }

    private static bool IsFree(Map map, Complex p) => map.ContainsKey(p) && map[p] != '#';

    private static bool IsRoad(Map map, Complex p) => IsFree(map, p) && Dirs.Count(d => IsFree(map, p + d)) == 2;

    private static Map ParseMap(string input)
    {
        var lines = input.Split('\n');
        return (
            from iRow in Enumerable.Range(0, lines.Length)
            from iCol in Enumerable.Range(0, lines[0].Length)
            let pos = new Complex(iCol, iRow)
            select new KeyValuePair<Complex, char>(pos, lines[iRow][iCol])
        ).ToDictionary();
    }

    private record Edge(Node Start, Node End, int Distance);
}
