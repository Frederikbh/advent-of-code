using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day03;

[ProblemName("Squares With Three Sides")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var triangles = ParseInput(input);

        return triangles.Count(triangle => IsValidTriangle(triangle[0], triangle[1], triangle[2]));
    }

    public object PartTwo(string input)
    {
        var triangles = ParseInput(input);
        var validTriangles = 0;

        for (var i = 0; i < triangles.Count; i += 3)
        {
            for (var j = 0; j < 3; j++)
            {
                if (IsValidTriangle(triangles[i][j], triangles[i + 1][j], triangles[i + 2][j]))
                {
                    validTriangles++;
                }
            }
        }

        return validTriangles;
    }

    private static List<int[]> ParseInput(string input) =>
        input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(
                line => line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
            .ToList();

    private static bool IsValidTriangle(int a, int b, int c) => a + b > c && a + c > b && b + c > a;
}
