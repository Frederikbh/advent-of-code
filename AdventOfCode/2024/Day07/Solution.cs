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
            if (IsValidEquation(result, numbers, []))
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
            var digitLengths = numbers.Select(GetDigitCount)
                .ToArray();

            if (IsValidEquation(result, numbers, digitLengths, concatenate: true))
            {
                sum += result;
            }
        }

        return sum;
    }

    private static bool IsValidEquation(
        long result,
        int[] numbers,
        int[] numbersLengths,
        int i = 0,
        long partialResult = 0L,
        bool concatenate = false)
    {
        if (i == numbers.Length)
        {
            return partialResult == result;
        }

        var currentNumber = numbers[i];
        var isValidWithOps = IsValidEquation(
                result,
                numbers,
                numbersLengths,
                i + 1,
                partialResult * currentNumber,
                concatenate) ||
            IsValidEquation(result, numbers, numbersLengths, i + 1, partialResult + currentNumber, concatenate);

        if (concatenate)
        {
            return isValidWithOps ||
                IsValidEquation(
                    result,
                    numbers,
                    numbersLengths,
                    i + 1,
                    Concatenate(partialResult, currentNumber, numbersLengths[i]),
                    concatenate);
        }

        return isValidWithOps;
    }

    private static long Concatenate(long a, long b, int digitsOfB) => a * (long)Math.Pow(10, digitsOfB) + b;

    private static int GetDigitCount(int num) => (int)Math.Log10(num) + 1;

    private static List<(long Result, int[])> ParseInput(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
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
}
