namespace AdventOfCode.Y2023.Day11;

[ProblemName("Cosmic Expansion")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var parsedInput = ParseInput(input);

        return Solve(parsedInput, 1);

    }

    public object PartTwo(string input) 
    {
        var parsedInput = ParseInput(input);

        return Solve(parsedInput, 999999);
    }

    private static long Solve(Input input, long emptyColumnAddition)
    {
        var sum = 0L;
        for (var i = 0; i < input.Galaxies.Count - 1; i++)
        {
            for (var j = i + 1; j < input.Galaxies.Count; j++)
            {
                var galaxy1 = input.Galaxies[i];
                var galaxy2 = input.Galaxies[j];

                var smallestX = Math.Min(galaxy1.X, galaxy2.X);
                var smallestY = Math.Min(galaxy1.Y, galaxy2.Y);

                var largestX = Math.Max(galaxy1.X, galaxy2.X);
                var largestY = Math.Max(galaxy1.Y, galaxy2.Y);

                long xDiff = Math.Abs(largestX - smallestX);
                long yDiff = Math.Abs(largestY - smallestY);

                foreach (var emptyColumn in input.EmptyColumns)
                {
                    if (emptyColumn > smallestX && emptyColumn < largestX)
                        xDiff += emptyColumnAddition;
                }

                foreach (var emptyRow in input.EmptyRows)
                {
                    if (emptyRow > smallestY && emptyRow < largestY)
                        yDiff += emptyColumnAddition;
                }

                sum += xDiff + yDiff;
            }
        }
        return sum;
    }

    private static Input ParseInput(string input)
    {
        var galaxies = new List<Point>();
        var lines = input.Split('\n');

        var emptyRows = new HashSet<int>();
        var emptyColumns = new HashSet<int>();

        for (var i = 0; i < lines.Length; i++)
        {
            var isEmpty = true;

            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#')
                {
                    isEmpty = false;
                    break;
                }
            }

            if(isEmpty)
                emptyRows.Add(i);
        }

        for (var i = 0; i < lines[0].Length; i++)
        {
            var isEmpty = true;

            for (var j = 0; j < lines.Length; j++)
            {
                if (lines[j][i] == '#')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
                emptyColumns.Add(i);
        }

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '#')
                {
                    galaxies.Add(new Point(j, i));
                }
            }
        }

        return new Input(galaxies, emptyRows, emptyColumns);
    }

    private record Input(List<Point> Galaxies, HashSet<int> EmptyRows, HashSet<int> EmptyColumns);
    private record Point(int X, int Y);
}
