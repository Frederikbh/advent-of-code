using System.Security.Cryptography;
using System.Text;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day05;

[ProblemName("How About a Nice Game of Chess?")]
public class Solution : ISolver
{
    public object PartOne(string input)
    {
        var found = 0;
        var index = 0;
        var password = new char[8];

        // Preallocate buffer for input + index
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var buffer = new byte[inputBytes.Length + 20]; // Enough for large indices
        Array.Copy(inputBytes, buffer, inputBytes.Length);

        using var md5 = MD5.Create();

        while (found < 8)
        {
            // Efficiently write index to buffer
            var indexLength = WriteIndexToBuffer(buffer, inputBytes.Length, index);

            // Compute MD5 hash
            var hash = md5.ComputeHash(buffer, 0, inputBytes.Length + indexLength);

            // Check if the first 20 bits are zero
            if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
            {
                // Extract the sixth character
                var c = (hash[2] & 0x0F).ToString("x")[0];
                password[found++] = c;
            }

            index++;
        }

        return new string(password);
    }

    public object PartTwo(string input)
    {
        var found = 0;
        var index = 0;
        var password = new char[8];

        // Preallocate buffer for input + index
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var buffer = new byte[inputBytes.Length + 20]; // Enough for large indices
        Array.Copy(inputBytes, buffer, inputBytes.Length);

        using var md5 = MD5.Create();

        while (found < 8)
        {
            // Efficiently write index to buffer
            var indexLength = WriteIndexToBuffer(buffer, inputBytes.Length, index);

            // Compute MD5 hash
            var hash = md5.ComputeHash(buffer, 0, inputBytes.Length + indexLength);

            // Check if the first 20 bits are zero
            if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
            {
                var position = hash[2] & 0x0F;
                if (position < 8 && password[position] == 0)
                {
                    // Extract the seventh character
                    var c = ((hash[3] & 0xF0) >> 4).ToString("x")[0];
                    password[position] = c;
                    found++;
                }
            }

            index++;
        }

        return new string(password);
    }

    // Efficiently writes an integer index into the buffer as ASCII bytes
    private static int WriteIndexToBuffer(byte[] buffer, int offset, int index)
    {
        var len = 0;
        var tempIndex = index;
        do
        {
            len++;
            tempIndex /= 10;
        } while (tempIndex > 0);

        var pos = offset + len - 1;
        tempIndex = index;
        do
        {
            buffer[pos--] = (byte)('0' + (tempIndex % 10));
            tempIndex /= 10;
        } while (tempIndex > 0);

        return len;
    }
}
