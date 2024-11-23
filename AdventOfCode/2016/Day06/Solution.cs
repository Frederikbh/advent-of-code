using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

using AdventOfCode.Lib;        

namespace AdventOfCode._2016.Day06;

[ProblemName("Signals and Noise")]
public class Solution : ISolver 
{

    public object PartOne(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        return DecodeMessage(lines, mostCommon: true);
    }

    public object PartTwo(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        return DecodeMessage(lines, mostCommon: false);
    }

    private static string DecodeMessage(string[] lines, bool mostCommon)
    {
        var length = lines[0].Length;
        var counts = new int[length][];
        for (var i = 0; i < length; i++)
        {
            counts[i] = new int[26]; // Assuming only lowercase letters 'a' to 'z'
        }

        foreach (var line in lines)
        {
            for (var i = 0; i < length; i++)
            {
                var c = line[i];
                counts[i][c - 'a']++;
            }
        }

        var result = new char[length];
        for (var i = 0; i < length; i++)
        {
            var columnCounts = counts[i];
            var targetCount = mostCommon ? -1 : int.MaxValue;
            var targetIndex = -1;
            for (var j = 0; j < 26; j++)
            {
                var count = columnCounts[j];
                if (count > 0)
                {
                    if (mostCommon)
                    {
                        if (count > targetCount)
                        {
                            targetCount = count;
                            targetIndex = j;
                        }
                    }
                    else
                    {
                        if (count < targetCount)
                        {
                            targetCount = count;
                            targetIndex = j;
                        }
                    }
                }
            }
            result[i] = (char)(targetIndex + 'a');
        }

        return new string(result);
    }
}
