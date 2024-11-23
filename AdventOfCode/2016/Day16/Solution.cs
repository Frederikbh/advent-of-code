using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day16;

[ProblemName("Dragon Checksum")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var a = Expand(input, 272);
        var checksum = Checksum(a);

        return checksum;
    }

    public object PartTwo(string input)
    {
        var a = Expand(input, 35651584);
        var checksum = Checksum(a);

        return checksum;
    }

    private static string Checksum(string input)
    {
        var length = input.Length;
        var checksum = input.ToCharArray();

        while (length % 2 == 0)
        {
            for (int i = 0,
                 j = 0;
                 i < length;
                 i += 2, j++)
            {
                checksum[j] = checksum[i] == checksum[i + 1]
                    ? '1'
                    : '0';
            }

            length /= 2;
        }

        return new string(checksum, 0, length);
    }

    private static string Expand(string input, int length)
    {
        var result = new char[length];
        var inputLength = input.Length;

        for (var i = 0; i < inputLength; i++)
        {
            result[i] = input[i];
        }

        var currentLength = inputLength;

        while (currentLength < length)
        {
            result[currentLength] = '0';

            for (var i = 0; i < currentLength && currentLength + 1 + i < length; i++)
            {
                result[currentLength + 1 + i] = result[currentLength - 1 - i] == '0'
                    ? '1'
                    : '0';
            }

            currentLength = Math.Min(currentLength * 2 + 1, length);
        }

        return new string(result);
    }
}
