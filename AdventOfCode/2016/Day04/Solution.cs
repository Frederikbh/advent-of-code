using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day04;

[ProblemName("Security Through Obscurity")]
public partial class Solution : ISolver
{
    public object PartOne(string input)
    {
        var lines = ParseInput(input);

        var validRooms = 0;

        foreach (var room in lines)
        {
            // Use an array of size 26 to store counts of each letter
            var counts = new int[26];

            foreach (var c in room.EncryptedName)
            {
                if (c != '-')
                {
                    counts[c - 'a']++;
                }
            }

            // Collect letters with their counts
            var letters = new List<char>();

            for (var i = 0; i < 26; i++)
            {
                if (counts[i] > 0)
                {
                    letters.Add((char)(i + 'a'));
                }
            }

            // Custom sort: first by count descending, then by letter ascending
            letters.Sort(
                (a, b) =>
                {
                    var countCompare = counts[b - 'a']
                        .CompareTo(counts[a - 'a']);

                    return countCompare != 0
                        ? countCompare
                        : a.CompareTo(b);
                });

            var checksum = new string(
                letters.Take(5)
                    .ToArray());

            if (checksum == room.Checksum)
            {
                validRooms += room.SectorId;
            }
        }

        return validRooms;
    }

    public object PartTwo(string input)
    {
        var lines = ParseInput(input);
        var textToFind = "northpole object storage".AsSpan();

        foreach (var line in lines)
        {
            var encryptedName = line.EncryptedName.AsSpan();

            // Early exit if lengths don't match
            if (encryptedName.Length != textToFind.Length)
            {
                continue;
            }

            var isMatch = true;
            for (var i = 0; i < encryptedName.Length; i++)
            {
                var decryptedChar = Decipher(encryptedName[i], line.SectorId);

                if (decryptedChar != textToFind[i])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                return line.SectorId;
            }
        }

        return 0;
    }

    private static char Decipher(char c, int shift)
    {
        if (c is '-')
        {
            return ' ';
        }

        return (char)('a' + (c - 'a' + shift) % 26);
    }

    public static List<Room> ParseInput(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var result = new List<Room>(lines.Length);

        foreach (var lineText in lines)
        {
            var line = lineText.AsSpan();
            var lastDashIndex = line.LastIndexOf('-');
            var encryptedName = line[..lastDashIndex];

            // Extract the sector ID
            var sectorId = int.Parse(line[(lastDashIndex + 1)..^7]);
            var checksum = line[^6..^1];

            result.Add(new Room
            {
                EncryptedName = encryptedName.ToString(),
                SectorId = sectorId,
                Checksum = checksum.ToString()
            });
        }

        return result;
    }
}

public struct Room
{
    public string EncryptedName;
    public int SectorId;
    public string Checksum;
}
