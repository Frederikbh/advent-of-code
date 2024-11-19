using AdventOfCode.Lib;

namespace AdventOfCode._2021.Day03;

[ProblemName("Binary Diagnostic")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var bitInput = ParseInput(input);
        var bitLength = bitInput[0].Length;
        var agg = Aggregate(bitInput);

        int gamma = 0,
            epsilon = 0;
        var halfCount = bitInput.Count / 2;

        // Build gamma and epsilon using bit manipulation
        // Shifts the bit into place
        for (var i = 0; i < bitLength; i++)
        {
            if (agg[i] > halfCount)
            {
                gamma |= 1 << (bitLength - i - 1);
            }
            else
            {
                epsilon |= 1 << (bitLength - i - 1);
            }
        }

        return gamma * epsilon;
    }

    public object PartTwo(string input)
    {
        var bitInput = ParseInput(input);

        // Oxygen generator rating value
        var oxygenValues = bitInput.ToList();
        var i = 0;

        while (oxygenValues.Count > 1)
        {
            var agg = AggregateMostCommon(oxygenValues);
            oxygenValues = oxygenValues.Where(line => line[i] == agg[i])
                .ToList();
            i++;
        }

        var co2Values = bitInput.ToList();
        i = 0;

        while (co2Values.Count > 1)
        {
            var agg = AggregateMostCommon(co2Values);
            co2Values = co2Values.Where(line => line[i] != agg[i])
                .ToList();
            i++;
        }

        var oxygen = Convert.ToInt32(new string(oxygenValues[0]), 2);
        var co2 = Convert.ToInt32(new string(co2Values[0]), 2);

        return oxygen * co2;
    }

    private static char[] AggregateMostCommon(List<char[]> bitInput)
    {
        var agg = Aggregate(bitInput);

        return agg.Select(
                e => e >= Math.Ceiling(bitInput.Count / 2d)
                    ? '1'
                    : '0')
            .ToArray();
    }

    private static int[] Aggregate(List<char[]> bitInput)
    {
        var bitLength = bitInput[0].Length;
        var agg = new int[bitLength];

        foreach (var line in bitInput)
        {
            for (var i = 0; i < bitLength; i++)
            {
                if (line[i] == '1')
                {
                    agg[i]++;
                }
            }
        }

        return agg;
    }

    private static List<char[]> ParseInput(string input) =>
        input
            .Split("\n")
            .Select(line => line.ToCharArray())
            .ToList();
}
