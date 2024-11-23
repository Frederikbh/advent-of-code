using AdventOfCode.Lib;

namespace AdventOfCode._2016.Day07;

[ProblemName("Internet Protocol Version 7")]
public class Solution : ISolver
{
    public object PartOne(string input) =>
        input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Count(SupportsTls);

    public object PartTwo(string input) =>
        input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Count(SupportsSsl);

    private static bool SupportsTls(string ip)
    {
        var parts = ip.Split('[', ']');
        var supportsTls = false;

        for (var i = 0; i < parts.Length; i++)
        {
            var part = parts[i];

            if (i % 2 == 0)
            {
                if (ContainsAbba(part))
                {
                    supportsTls = true;
                }
            }
            else
            {
                if (ContainsAbba(part))
                {
                    return false;
                }
            }
        }

        return supportsTls;
    }

    private static bool SupportsSsl(string ip)
    {
        var parts = ip.Split('[', ']');
        var abas = new List<string>();
        var babs = new HashSet<string>();

        for (var i = 0; i < parts.Length; i++)
        {
            var part = parts[i];

            for (var j = 0; j < part.Length - 2; j++)
            {
                if (part[j] == part[j + 2] && part[j] != part[j + 1])
                {
                    if (i % 2 == 0)
                    {
                        abas.Add(part[j..(j + 3)]);
                    }
                    else
                    {
                        babs.Add(part[j..(j + 3)]);
                    }
                }
            }
        }

        foreach (var aba in abas)
        {
            if (babs.Contains($"{aba[1]}{aba[0]}{aba[1]}"))
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsAbba(string s)
    {
        for (var i = 0; i < s.Length - 3; i++)
        {
            if (s[i] == s[i + 3] && s[i + 1] == s[i + 2] && s[i] != s[i + 1])
            {
                return true;
            }
        }

        return false;
    }
}
