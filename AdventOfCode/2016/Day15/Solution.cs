using System.Text.RegularExpressions;

using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day15;

[ProblemName("Timing is Everything")]
public partial class Solution : ISolver
{
    public object PartOne(string input)
    {
        var discs = ParseInput(input);

        var moduli = discs.Select(disc => (long)disc.Positions)
            .ToList();
        var remainders = discs.Select(disc => Mod(-disc.InitialPosition - disc.Depth, disc.Positions))
            .ToList();

        var t = ChineseRemainderTheorem(remainders, moduli);

        return t;
    }

    public object PartTwo(string input)
    {
        var discs = ParseInput(input);
        discs.Add(new Disc(11, 0, discs.Count + 1));

        var moduli = discs.Select(disc => (long)disc.Positions)
            .ToList();
        var remainders = discs.Select(disc => Mod(-disc.InitialPosition - disc.Depth, disc.Positions))
            .ToList();

        var t = ChineseRemainderTheorem(remainders, moduli);

        return t;
    }

    private static List<Disc> ParseInput(string input)
    {
        var regex = DiscRegex();

        return input
            .Split("\n")
            .Select(line => regex.Match(line))
            .Select(
                match => new Disc(
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value),
                    int.Parse(match.Groups[1].Value)))
            .ToList();
    }

    [GeneratedRegex(@"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+).")]
    public static partial Regex DiscRegex();

    private static long Mod(long a, long m)
    {
        var result = a % m;

        return result < 0
            ? result + m
            : result;
    }

    private static long ChineseRemainderTheorem(List<long> remainders, List<long> moduli)
    {
        long prod = 1;

        foreach (var mod in moduli)
        {
            prod *= mod;
        }

        long result = 0;

        for (var i = 0; i < remainders.Count; i++)
        {
            var p = prod / moduli[i];
            var inv = ModInverse(p, moduli[i]);
            result += remainders[i] * inv * p;
        }

        return result % prod;
    }

    private static long ModInverse(long a, long m)
    {
        var (g, x, _) = ExtendedGcd(a, m);

        if (g != 1)
        {
            throw new Exception("Modular inverse does not exist");
        }

        return (x % m + m) % m;
    }

    private static (long gcd, long x, long y) ExtendedGcd(long a, long b)
    {
        if (a == 0)
        {
            return (b, 0, 1);
        }
        else
        {
            var (gcd, x1, y1) = ExtendedGcd(b % a, a);
            var x = y1 - b / a * x1;
            var y = x1;

            return (gcd, x, y);
        }
    }
}

internal record Disc(int Positions, int InitialPosition, int Depth);
