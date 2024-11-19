using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day14;

[ProblemName("Parabolic Reflector Dish")]
public class Solution : ISolver
{
    private const char RoundRock = 'O';
    private const char CubeRock = '#';
    private const char Ground = '.';

    public object PartOne(string input) 
    {
        var grid = ParseInput(input);

        grid.TiltNorth();

        return grid.Area.Select(t => t.Count(e => e == RoundRock))
            .Select((rockCount, i) => rockCount * (grid.Area.Length - i))
            .Sum();
    }

    public object PartTwo(string input)
    {
        var grid = ParseInput(input);

        var cache = new Dictionary<string, int>();
        var cycle = 1;

        while (cycle <= 1_000_000_000)
        {
            grid.TiltNorth();
            grid.TiltWest();
            grid.TiltSouth();
            grid.TiltEast();

            var current = grid.CacheKey();

            if (cache.TryGetValue(current, out var cached))
            {
                var remaining = 1_000_000_000 - cycle - 1;
                var loop = cycle - cached;

                var loopRemaining = remaining % loop;
                cycle = 1_000_000_000 - loopRemaining - 1;
            }

            cache[current] = cycle++;
        }

        return grid.Area.Select(t => t.Count(e => e == RoundRock))
            .Select((rockCount, i) => rockCount * (grid.Area.Length - i))
            .Sum();
    }

    private static Grid ParseInput(string input)
    {
        var lines = input.Split("\n");
        var area = new char[lines.Length][];

        for (var i = 0; i < lines.Length; i++)
        {
            area[i] = new char[lines[i].Length];

            for (var j = 0; j < lines[i].Length; j++)
            {
                area[i][j] = lines[i][j];
            }
        }
        return new Grid(area);
    }

    private record Grid(char[][] Area)
    {
        public string CacheKey()
        {
            return string.Join('\n', Area.Select(t => new string(t)));
        }

        public void TiltNorth()
        {
            for (var row = 1; row < Area.Length; row++)
            {
                for (var col = 0; col < Area[row].Length; col++)
                {
                    var c = Area[row][col];

                    if (c != RoundRock)
                        continue;

                    var previous = 1;

                    while (Area[row - previous][col] == '.')
                    {
                        Area[row - previous][col] = RoundRock;
                        Area[row - previous + 1][col] = Ground;
                        previous++;

                        if (row - previous < 0)
                            break;
                    }
                }
            }
        }

        public void TiltSouth()
        {
            for (var row = Area.Length - 2; row >= 0; row--)
            {
                for (var col = 0; col < Area[row].Length; col++)
                {
                    var c = Area[row][col];

                    if (c != RoundRock)
                        continue;

                    var previous = 1;

                    while (Area[row + previous][col] == '.')
                    {
                        Area[row + previous][col] = RoundRock;
                        Area[row + previous - 1][col] = Ground;
                        previous++;

                        if (row + previous >= Area.Length)
                            break;
                    }
                }
            }
        }

        public void TiltWest()
        {
            for (var row = 0; row < Area.Length; row++)
            {
                for (var col = 1; col < Area[row].Length; col++)
                {
                    var c = Area[row][col];

                    if (c != RoundRock)
                        continue;

                    var previous = 1;

                    while (Area[row][col - previous] == '.')
                    {
                        Area[row][col - previous] = RoundRock;
                        Area[row][col - previous + 1] = Ground;
                        previous++;

                        if (col - previous < 0)
                            break;
                    }
                }
            }
        }

        public void TiltEast()
        {
            for (var row = 0; row < Area.Length; row++)
            {
                for (var col = Area[row].Length - 2; col >= 0; col--)
                {
                    var c = Area[row][col];

                    if (c != RoundRock)
                        continue;

                    var previous = 1;

                    while (Area[row][col + previous] == '.')
                    {
                        Area[row][col + previous] = RoundRock;
                        Area[row][col + previous - 1] = Ground;
                        previous++;

                        if (col + previous >= Area[row].Length)
                            break;
                    }
                }
            }
        }
    }
}
