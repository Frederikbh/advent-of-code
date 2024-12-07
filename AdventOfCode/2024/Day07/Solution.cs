using AdventOfCode.Lib;

namespace AdventOfCode._2024.Day07;

[ProblemName("Bridge Repair")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var equations = ParseInput(input);
        var sum = 0L;

        foreach (var (result, numbers) in equations)
        {
            var digits = numbers.Select(GetDigitCount)
                .ToArray();

            if (IsValidEquationBackward(result, numbers, digits, numbers.Length - 1, false))
            {
                sum += result;
            }
        }

        return sum;
    }

    public object PartTwo(string input)
    {
        var equations = ParseInput(input);
        var sum = 0L;

        foreach (var (result, numbers) in equations)
        {
            var digits = numbers.Select(GetDigitCount)
                .ToArray();

            if (IsValidEquationBackward(result, numbers, digits, numbers.Length - 1, true))
            {
                sum += result;
            }
        }

        return sum;
    }

    private static bool IsValidEquationBackward(
        long target,
        int[] numbers,
        int[] numbersLengths,
        int i,
        bool concatenate)
    {
        if (i < 0)
        {
            return target == 0;
        }

        var n = numbers[i];
        var digits = numbersLengths[i];

        // Try concatenation if allowed and if target ends with the digits of n
        if (concatenate)
        {
            var pow = Pow10(digits);

            if (target % pow == n)
            {
                var prev = target / pow;

                if (IsValidEquationBackward(prev, numbers, numbersLengths, i - 1, concatenate))
                {
                    return true;
                }
            }
        }

        // Try multiplication if divisible
        if (n != 0 && target % n == 0)
        {
            var prev = target / n;

            if (IsValidEquationBackward(prev, numbers, numbersLengths, i - 1, concatenate))
            {
                return true;
            }
        }

        // Try addition (always possible)
        {
            var prev = target - n;

            if (IsValidEquationBackward(prev, numbers, numbersLengths, i - 1, concatenate))
            {
                return true;
            }
        }

        return false;
    }

    private static List<(long Result, int[])> ParseInput(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var results = new List<(long Result, int[])>();

        foreach (var line in lines)
        {
            var split = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var result = long.Parse(split[0]);
            var parts = split[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            results.Add((result, parts));
        }

        return results;
    }

    private static int GetDigitCount(int num) => (int)Math.Log10(num) + 1;

    private static long Pow10(int digits)
    {
        long result = 1;

        for (var i = 0; i < digits; i++)
        {
            result *= 10;
        }

        return result;
    }
}
