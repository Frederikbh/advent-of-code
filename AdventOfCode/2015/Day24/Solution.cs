using System.Collections.Immutable;

using AdventOfCode.Lib;

namespace AdventOfCode._2015.Day24;

[ProblemName("It Hangs in the Balance")]
public class Solution : ISolver
{
    public object PartOne(string input) => Solve(Parse(input), 3);

    public object PartTwo(string input) => Solve(Parse(input), 4);

    private int[] Parse(string input) =>
        input.Split("\n")
            .Select(int.Parse)
            .ToArray();

    private static long Solve(int[] numbers, int groups)
    {
        var mul = (ImmutableList<int> l) => l.Aggregate(1L, (m, x) => m * x);

        for (var i = 0; i < numbers.Length; i++)
        {
            var parts = Pick(numbers, i, 0, numbers.Sum() / groups)
                .ToList();

            if (parts.Count != 0)
            {
                return parts.Select(mul)
                    .Min();
            }
        }

        throw new Exception();
    }

    private static IEnumerable<ImmutableList<int>> Pick(int[] numbers, int count, int i, int sum)
    {
        while (true)
        {
            if (sum == 0)
            {
                yield return ImmutableList.Create<int>();

                yield break;
            }

            if (count < 0 || sum < 0 || i >= numbers.Length)
            {
                yield break;
            }

            if (numbers[i] <= sum)
            {
                foreach (var x in Pick(numbers, count - 1, i + 1, sum - numbers[i]))
                {
                    yield return x.Add(numbers[i]);
                }
            }

            i += 1;
        }
    }
}
