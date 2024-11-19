using System.Text;
using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2023.Day15;

[ProblemName("Lens Library")]
public partial class Solution : ISolver
{
    [GeneratedRegex(@"(\w+)([-=])(\d*)")]
    public partial Regex LensParser();

    public object PartOne(string input)
    {
        return input
            .Split(',')
            .Select(Hash)
            .Sum();
    }

    public static int Hash(string input)
    {
        return Encoding.ASCII.GetBytes(input)
            .Aggregate(0, (current, getByte) => (current + getByte) * 17 % 256);
    }

    public object PartTwo(string input)
    {
        var lenses = ParseLenses(input);
        var boxes = Enumerable.Range(0, 256)
            .Select(_ => new List<Lens>())
            .ToList();

        foreach (var lens in lenses)
        {
            if (lens.Operation == '=')
            {
                var replaced = false;
                for (var i = 0; i < boxes[lens.Hash].Count; i++)
                {
                    if (boxes[lens.Hash][i].Label == lens.Label)
                    {
                        boxes[lens.Hash][i] = lens;
                        replaced = true;
                        break;
                    }
                }

                if (!replaced)
                {
                    boxes[lens.Hash].Add(lens);
                }
            }
            else
            {
                boxes[lens.Hash] = boxes[lens.Hash]
                    .Where(e => e.Label != lens.Label)
                    .ToList();
            }
        }

        var total = 0;

        for (var i = 0; i < boxes.Count; i++)
        {
            for (var j = 0; j < boxes[i].Count; j++)
            {
                var val = (i + 1) * (j + 1) * boxes[i][j].FocalLength ?? 0;
                total += val;
            }
        }

        return total;
    }

    private List<Lens> ParseLenses(string input)
    {
        var lensParser = LensParser();
        return input.Split(',')
            .Select(e => lensParser.Match(e))
            .Select(
                e =>
                {
                    var focalLength = e.Groups[3].Value == "" ? null : (int?)int.Parse(e.Groups[3].Value);
                    return new Lens(e.Groups[1].Value, e.Groups[2].Value[0], focalLength, Hash(e.Groups[1].Value));
                })
            .ToList();
    }

    private record Lens(string Label, char Operation, int? FocalLength, int Hash);
}
