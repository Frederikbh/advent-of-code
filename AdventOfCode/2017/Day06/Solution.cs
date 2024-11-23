using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day06;

[ProblemName("Memory Reallocation")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var banks = ParseInput(input);
        var seen = new HashSet<string>();
        var cycles = 0;

        while (true)
        {
            Redistribute(banks);

            var state = GetStateKey(banks);

            cycles++;
            if (!seen.Add(state))
            {
                return cycles;
            }
        }
    }

    public object PartTwo(string input)
    {
        var banks = ParseInput(input);
        var seen = new Dictionary<string, int>();
        var cycles = 0;

        while (true)
        {
            Redistribute(banks);

            var state = GetStateKey(banks);

            if (seen.TryGetValue(state, out var firstCycle))
            {
                return cycles - firstCycle;
            }

            seen[state] = cycles;
            cycles++;
        }
    }

    private static int[] ParseInput(string input) =>
        input.Split('\t')
            .Select(int.Parse)
            .ToArray();

    private static void Redistribute(int[] banks)
    {
        var maxBankIndex = Array.IndexOf(banks, banks.Max());
        var blocks = banks[maxBankIndex];
        banks[maxBankIndex] = 0;

        for (var i = 0; i < blocks; i++)
        {
            banks[(maxBankIndex + i + 1) % banks.Length]++;
        }
    }

    private static string GetStateKey(int[] banks) => string.Join(",", banks);
}
