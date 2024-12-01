using System.Text;

using AdventOfCode.Lib;

namespace AdventOfCode._2017.Day10;

[ProblemName("Knot Hash")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var lengths = ParseInput(input);

        var list = KnotHash(lengths, 1);

        return list[0] * list[1];
    }

    public object PartTwo(string input)
    {
        var suffix = new[]
        {
            17,
            31,
            73,
            47,
            23
        };
        var lengths = input.ToCharArray()
            .Select(c => (int)c)
            .Concat(suffix)
            .ToArray();

        var list = KnotHash(lengths, 64);

        return SparseHash(list);
    }

    private static int[] KnotHash(int[] lengths, int rounds)
    {
        var list = Enumerable.Range(0, 256)
            .ToArray();
        var position = 0;
        var skip = 0;

        for (var round = 0; round < rounds; round++)
        {
            foreach (var length in lengths)
            {
                position = Hash(list, position, length, skip++);
            }
        }

        return list;
    }

    private static string SparseHash(int[] list)
    {
        var sb = new StringBuilder(32);

        for (var i = 0; i < list.Length; i += 16)
        {
            var xor = list[i];

            for (var j = 1; j < 16; j++)
            {
                xor ^= list[i + j];
            }

            sb.Append(xor.ToString("x2"));
        }

        return sb.ToString();
    }

    public static int[] ParseInput(string input) =>
        input.Split(',')
            .Select(int.Parse)
            .ToArray();

    public static int Hash(int[] list, int position, int length, int skip)
    {
        var listLength = list.Length;

        for (var i = 0; i < length / 2; i++)
        {
            var a = (position + i) % listLength;
            var b = (position + length - i - 1) % listLength;
            (list[a], list[b]) = (list[b], list[a]);
        }

        return (position + length + skip) % list.Length;
    }
}
