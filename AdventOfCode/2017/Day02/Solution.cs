using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

using AdventOfCode.Lib;        

namespace AdventOfCode._2017.Day02;

[ProblemName("Corruption Checksum")]
public class Solution : ISolver 
{

    public object PartOne(string input) 
    {
        var sheet = ParseInput(input);
        var sum = 0;
        foreach (var row in sheet)
        {
            sum += row.Max() - row.Min();
        }
        return sum;
    }

    public object PartTwo(string input) 
    {
        var sheet = ParseInput(input);
        var sum = 0;
        foreach (var row in sheet)
        {
            for (var i = 0; i < row.Length; i++)
            {
                for (var j = 0; j < row.Length; j++)
                {
                    if (i != j && row[i] % row[j] == 0)
                    {
                        sum += row[i] / row[j];
                    }
                }
            }
        }
        return sum;
    }

    private static int[][] ParseInput(string input)
    {
        return input
            .Split('\n')
            .Select(row => row
                .Split('\t')
                .Select(int.Parse)
                .ToArray()
            )
            .ToArray();
    }
}
